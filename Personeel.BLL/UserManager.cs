using Personeel.DTO;
using Personeel.IBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.Models;
using System.Data.Entity;
using Personeel.DAL;
using Personeel.IDAL;

namespace Personeel.BLL
{
    public class UserManager : IUserManager
    {
        public async Task AddUser(string email, string password, string name, Guid userRight,int basicMoney,Guid departmentId,Guid positionId)
        {
            using(IDAL.IUserService userService = new DAL.UserService())
            {
                    
                     await  userService.CreateAsync(new User 
                   {  
                     Email = email,
                     Password=password,
                     Name=name,
                     UserRightID= userRight,
                     ImagePath="default.png",
                     Basicmoney = basicMoney,
                     Beginworkdate = DateTime.Now,
                     DepartmentID = departmentId,
                     PositionID = positionId
                   });
            }
        }

        public async Task ChangeInfo(Guid id,string email,string name, string gender, DateTime birthday, int idNum, string wedlock, string race, string nativePlace, string politic, int phone, string tipTopDegree, string school)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                var user = await userService.GetAllAsync().FirstAsync(m => m.Id == id);
                user.Email = email;
                user.Name = name;
                user.Gender = gender;
                user.Birthday = birthday;
                user.IDNumber = idNum;
                user.Wedlock = wedlock;
                user.Race = race;
                user.Politic = politic;
                user.Phone = phone;
                user.Nativeplace = nativePlace;
                user.Tiptopdegree = tipTopDegree;
                user.School = school;
                await userService.EditAsync(user);
            }
        }

        public async Task ChangePassword(Guid id,string email, string oldPwd, string newPwd)
        {
            
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                if (await userService.GetAllAsync().AnyAsync(m => m.Email == email && m.Password == oldPwd))
                {
                    var user = await userService.GetAllAsync().FirstAsync(m => m.Email == email);
                    user.Password = newPwd;
                    await userService.EditAsync(user);
                }
            }
        }

        public async Task<List<UserInfoDto>> GetAllUser()
        {
            using (IUserService userService=new UserService())
            {
                return  await userService.GetAllAsync().Where(m => m.IsRemoved == false).Select(m =>
                    new DTO.UserInfoDto()
                    {
                        UserId = m.Id,
                        Email = m.Email,
                        ImagePath = m.ImagePath,
                        UserNum = m.UserNum,
                        Name = m.Name,
                        Gender = m.Gender,
                        Birthday = m.Birthday,
                        IdNumber = m.IDNumber,
                        Wedlock = m.Wedlock,
                        Race = m.Race,
                        NativePlace = m.Nativeplace,
                        Phone = m.Phone,
                        Politic = m.Politic,
                        School = m.School,
                        TipTopDegree = m.Tiptopdegree,
                        BeginWorkDate = m.Beginworkdate,
                        BeFormDate = m.Beformdate,
                        NotWorkDate = m.Notworkdate,
                        Department = m.Department.Depname,
                        Position = m.Position.Posname,
                        BasicMoney = m.Basicmoney,
                        UserPower = m.UserRight.UserPower,
                        UserRightId = m.UserRightID,
                        IsManager = m.IsManager,
                        DepGuid=m.DepartmentID
                    }).ToListAsync();
            }
        }

        public async Task<List<DTO.UserInfoDto>> GetAllUserByPage(int pageIndex, int pageSize)
        {
            using (IUserService userService = new UserService())
            {
                return await userService.GetAllByPageOrderAsync(pageSize, pageIndex, false).Where(m => m.IsRemoved == false).Select(m =>
                   new DTO.UserInfoDto()
                   {
                       UserId = m.Id,
                       Email = m.Email,
                       ImagePath = m.ImagePath,
                       UserNum = m.UserNum,
                       Name = m.Name,
                       Gender = m.Gender,
                       Birthday = m.Birthday,
                       IdNumber = m.IDNumber,
                       Wedlock = m.Wedlock,
                       Race = m.Race,
                       NativePlace = m.Nativeplace,
                       Phone = m.Phone,
                       Politic = m.Politic,
                       School = m.School,
                       TipTopDegree = m.Tiptopdegree,
                       BeginWorkDate = m.Beginworkdate,
                       BeFormDate = m.Beformdate,
                       NotWorkDate = m.Notworkdate,
                       Department = m.Department.Depname,
                       Position = m.Position.Posname,
                       BasicMoney = m.Basicmoney,
                       UserPower = m.UserRight.UserPower,
                       UserRightId = m.UserRightID,
                       IsManager = m.IsManager
                   }).ToListAsync();
            }
        }
        public async Task<int> GetDataCount()
        {
            using (IUserService userService = new UserService())
            {
                return await userService.GetAllAsync().CountAsync();
            }
        }

        public async Task<UserInfoDto> GetUserById(Guid id)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                if (await userService.GetAllAsync().AnyAsync(m => m.Id==id ))
                {
                    return await userService.GetAllAsync().Where(m => m.Id == id).Select(m =>
                        new DTO.UserInfoDto()
                        {
                            UserId = m.Id,
                            Email = m.Email,
                            ImagePath = m.ImagePath,
                            UserNum = m.UserNum,
                            Name = m.Name,
                            Gender = m.Gender,
                            Birthday = m.Birthday,
                            IdNumber = m.IDNumber,
                            Wedlock = m.Wedlock,
                            Race = m.Race,
                            NativePlace = m.Nativeplace,
                            Phone = m.Phone,
                            Politic = m.Politic,
                            School = m.School,
                            TipTopDegree = m.Tiptopdegree,
                            BeginWorkDate = m.Beginworkdate,
                            BeFormDate = m.Beformdate,
                            NotWorkDate = m.Notworkdate,
                            Department = m.Department.Depname,
                            Position = m.Position.Posname,
                            BasicMoney = m.Basicmoney,
                            UserPower = m.UserRight.UserPower,
                            UserRightId = m.UserRightID,
                            DepGuid = m.DepartmentID,
                            IsManager = m.IsManager
                        }).FirstAsync();
                }
                else
                {
                    throw new ArgumentException(message: "ID不存在");
                }
            }
        }

        public async Task DeleteUser(Guid id)
        {
            using (IUserService userService=new UserService())
            {
                User user=await userService.GetAllAsync().Where(m => m.Id == id).FirstAsync();
                await userService.RemoveAsync(user.Id);
            }
        }
        public bool Login(string email, string password,out Guid userId,out string userName,out Guid userRight, out bool isManager)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                var user=userService.GetAllAsync().FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
                user.Wait();
                var data = user.Result;
                if (data == null)
                {
                    userName = "用户";
                    userId = new Guid();
                    userRight = new Guid();
                    isManager = false;
                    return false;
                }
                else
                {
                    userId = data.Id;
                    userName = data.Name;
                    userRight = data.UserRightID;
                    isManager = data.IsManager;
                    return true;
                }
            }
        }

        public async Task<UserInfoDto> GetUserByEmail(string email)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                if (await userService.GetAllAsync().AnyAsync(m => m.Email == email))
                {
                    return await userService.GetAllAsync().Where(m => m.Email == email).Select(m =>
                        new DTO.UserInfoDto()
                        {
                            UserId = m.Id,
                            Email = m.Email,
                            ImagePath = m.ImagePath,
                            UserNum = m.UserNum,
                            Name = m.Name,
                            Gender = m.Gender,
                            Birthday = m.Birthday,
                            IdNumber = m.IDNumber,
                            Wedlock = m.Wedlock,
                            Race = m.Race,
                            NativePlace = m.Nativeplace,
                            Phone = m.Phone,
                            Politic = m.Politic,
                            School = m.School,
                            TipTopDegree = m.Tiptopdegree,
                            BeginWorkDate = m.Beginworkdate,
                            BeFormDate = m.Beformdate,
                            NotWorkDate = m.Notworkdate,
                            Department = m.Department.Depname,
                            Position = m.Position.Posname,
                            BasicMoney = m.Basicmoney,
                            UserPower = m.UserRight.UserPower,
                            UserRightId = m.UserRightID,
                            IsManager = m.IsManager
                        }).FirstAsync();
                }
                else
                {
                    throw new ArgumentException(message: "ID不存在");
                }
            }
        }

        public async Task ChangePower(Guid id, Guid powerId)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                var user = await userService.GetAllAsync().FirstAsync(m => m.Id == id);
                user.UserRightID = powerId;
                await userService.EditAsync(user);
            }
        }

        public async Task ChangeIsManager(Guid id, bool isManager)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                var user = await userService.GetAllAsync().FirstAsync(m => m.Id == id);
                user.IsManager = isManager;
                await userService.EditAsync(user);
            }
        }
    }
}
