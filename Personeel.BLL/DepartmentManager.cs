using Personeel.DAL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.Models;
using System.Data.Entity;

namespace Personeel.BLL
{
    public class DepartmentManager :IDepartmentManager
    {
        #region 部门管理

        public async Task AddDep(string depName, string depDes,Guid depUser)
        {
            using(IDepartmentService departmentService=new DepartmentService())
            {
                await departmentService.CreateAsync(new Department()
                {
                    Depname = depName,
                    Depdescribe = depDes,
                    UserGuid = depUser
                });
            }
        }

        public async Task EditDep(Guid depGuid,string depName, string depDes,Guid depUser)
        {
            using(IDepartmentService departmentService=new DepartmentService())
            {
                var Dep = await departmentService.GetAllAsync().FirstAsync(m => m.Id==depGuid);
                Dep.Depname = depName;
                Dep.Depdescribe = depDes;
                Dep.UserGuid = depUser;
                await departmentService.EditAsync(Dep);
            }
        }

        public async Task<List<DepInfoDto>> GetInfo()
        {
            using (IDepartmentService departmentService = new DepartmentService())
            {
                return await departmentService.GetAllAsync().Select(m => new DTO.DepInfoDto()
                {
                    DepGuid = m.Id,
                    DepName=m.Depname,
                    DepDes=m.Depdescribe,
                    DepUserGuid = m.UserGuid
                }).ToListAsync();

            }
        }

        public async Task<DepInfoDto> GetInfoById(Guid id)
        {
            using (IDepartmentService departmentService = new DepartmentService())
            {
                    return await departmentService.GetAllAsync()
                    .Where(m => m.Id == id).Select(m => new DepInfoDto()
                    {
                        DepGuid = m.Id,
                        DepName = m.Depname,
                        DepDes = m.Depdescribe,
                        DepUserGuid = m.UserGuid
                    }).FirstAsync();
            }
        }

        public async Task<DepInfoDto> GetInfoByName(string depName)
        {
            using (IDepartmentService departmentService = new DepartmentService())
            {
                return await departmentService.GetAllAsync()
                    .Where(m => m.Depname == depName).Select(m => new DepInfoDto()
                    {
                        DepGuid = m.Id,
                        DepName = m.Depname,
                        DepDes = m.Depdescribe,
                        DepUserGuid = m.UserGuid
                    }).FirstAsync();

            }
        }
        public async Task RemoveDep(Guid id)
        {
            using (IDepartmentService departmentService = new DepartmentService())
            {
                await departmentService.RemoveAsync(id);
            }


        }
        #endregion

        #region 调动管理
        /// <summary>
        /// 获取所有用户的部门岗位信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChangeDepartmentInfoDto>> GetAllUserDepartment()
        {
            using (IUserService userService=new UserService())
            {
                return await userService.GetAllAsync().Select(m => new ChangeDepartmentInfoDto()
                {
                    UserId = m.Id,
                    UserName = m.Name,
                    BeforeDep = m.Department.Depname,
                    BeforePos = m.Position.Posname
                }).ToListAsync();
            }
        }
        /// <summary>
        /// 获取所有调薪记录
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ChangeDepartmentInfoDto>> GetAllTransferRecords()
        {
            using (ITransferService transferService = new TransferService())
            {
                return await transferService.GetAllAsync().Select(m => new ChangeDepartmentInfoDto()
                {
                    Id = m.Id,
                    UserName = m.User.Name,
                    BeforeDep = m.BeforeTransferDep,
                    BeforePos = m.BeforeTransferPos,
                    TransferDep = m.TransferDep,
                    TransferPos = m.TransferPos,
                    TransferDateTime = m.CreateTime,
                    TransferReason = m.TransferReason
                }).ToListAsync();
            }
        }

        public async Task<ChangeDepartmentInfoDto> GetOneUserDepartment(Guid userGuid)
        {
            using (IUserService userService = new UserService())
            {
              return  await userService.GetAllAsync().Where(m => m.Id == userGuid).Select(m => new ChangeDepartmentInfoDto()
                {
                    UserName = m.Name,
                    BeforeDep = m.Department.Depname,
                    BeforePos = m.Position.Posname
                }).FirstAsync();
               
            }
        }

        public async Task CreateTransferInfo(Guid userGuid, string transferDep, string transferPos,string reason)
        {
            using (ITransferService transferService = new TransferService())
            {
                using (IUserService userService = new UserService())
                {
                    User user = await userService.GetAllAsync().Where(m => m.Id == userGuid).FirstAsync();
                    string depName= await userService.GetAllAsync().Where(m=>m.Id==userGuid).Select(m => m.Department.Depname).FirstAsync();
                    string posName = await userService.GetAllAsync().Where(m => m.Id == userGuid).Select(m => m.Position.Posname).FirstAsync();
                    await transferService.CreateAsync(new Transfer()
                    {
                        UserId = userGuid,
                        BeforeTransferPos = posName,
                        BeforeTransferDep = depName,
                        TransferDep = transferDep,
                        TransferPos = transferPos,
                        TransferReason = reason
                    });
                    using (IPositionService positionService = new PositionService())
                    {
                        using (IDepartmentService departmentService = new DepartmentService())
                        {
                            Guid departGuid = await departmentService.GetAllAsync().Where(m => m.Depname == transferDep)
                                .Select(m => m.Id).FirstAsync();
                            Guid posGuid = await positionService.GetAllAsync().Where(m => m.Posname == transferPos)
                                .Select(m => m.Id).FirstAsync();
                            user.PositionID = posGuid;
                            user.DepartmentID = departGuid;
                        }
                    }
                    await userService.EditAsync(user);
                }
            }
        }

        public async Task RemoveTransferInfo(Guid id)
        {
            using (ITrainService trainService = new TrainService())
            {
               await trainService.RemoveAsync(id);
            }
        }


        #endregion
        
    }
}
