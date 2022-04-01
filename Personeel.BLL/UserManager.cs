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

namespace Personeel.BLL
{
    public class UserManager : IUserManager
    {
        public async Task AddUser(string email, string password, string name, int userRight,int basicMoney)
        {
            using (IDAL.IUserRightService userRightService=new DAL.UserRightService())
            {
                using(IDAL.IUserService userService = new DAL.UserService())
                {
                    var right = await userRightService.GetAllAsync().FirstAsync(m => m.UserPower == userRight);
                     await  userService.CreateAsync(new User 
                   {  
                     Email = email,
                     Password=password,
                     Name=name,
                     UserRightID= right.Id,
                     ImagePath="default.png",
                     Basicmoney = basicMoney,
                     Beginworkdate = DateTime.Now,
                   });
                }
            }
            
        }

        public async Task ChangeInfo(string email,string name, string imagePath, string gender, DateTime birthday, int idNum, string wedlock, string race, string nativePlace, string politic, int phone, string tipTopDegree, string school)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                var user = await userService.GetAllAsync().FirstAsync(m => m.Email == email);
                user.Name = name;
                user.ImagePath = imagePath;
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

        public async Task ChangePassword(string email, string oldPwd, string newPwd)
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

        public async Task<UserInfoDto> GetUserByEmail(string email)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                if (await userService.GetAllAsync().AnyAsync(m => m.Email == email ))
                {
                    return await userService.GetAllAsync().Where(m => m.Email == email).Select(m =>
                        new DTO.UserInfoDto()
                        {
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
                            BasicMoney = m.Basicmoney
                        }).FirstAsync();
                }
                else
                {
                    throw new ArgumentException(message: "邮箱地址不存在");
                }
            }
        }

        public async Task<bool> Login(string email, string password,int userRight)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                return await userService.GetAllAsync().AnyAsync(m => m.Email == email && m.Password == password&&m.UserRight.UserPower==userRight);
            }
        }
    }
}
