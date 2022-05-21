using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DAL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.BLL
{
    public class AskForLeaveManager : IAskForLeaveManager
    {
        public async Task AddAskForLeave(Guid userGuid, string leaveSort, string leaveReason, DateTime startDate,
            DateTime endDate)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                using (IUserService userService = new UserService())
                {
                    int power = await userService.GetAllAsync().Where(m => m.Id == userGuid)
                        .Select(m => m.UserRight.UserPower).FirstAsync();
                    bool isManager= await userService.GetAllAsync().Where(m => m.Id == userGuid)
                        .Select(m => m.IsManager).FirstAsync();
                    Guid depGuid = await userService.GetAllAsync().Where(m => m.Id == userGuid)
                        .Select(m => m.DepartmentID).FirstAsync();
                    if (power == 3&&!isManager)
                    {
                        await askForLeaveService.CreateAsync(new AskForLeave()
                        {
                            ApproveTime = DateTime.Now,
                            DepId = depGuid,
                            UserId = userGuid,
                            LeaveSort = leaveSort,
                            LeaveReason = leaveReason,
                            StartTime = startDate,
                            EndTime = endDate,
                            LeaveState = (int)AskState.部门主管审批
                        });
                    }
                    else
                    {
                        await askForLeaveService.CreateAsync(new AskForLeave()
                        {
                            ApproveTime = DateTime.Now,
                            DepId = depGuid,
                            UserId = userGuid,
                            LeaveSort = leaveSort,
                            LeaveReason = leaveReason,
                            StartTime = startDate,
                            EndTime = endDate,
                            LeaveState = (int)AskState.人事审批
                        });
                    }
                }
                
            }
        }

        public async Task<List<AskLeaveInfoDto>> GetAllAskByUserId(Guid userGuid)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {

                List<AskForLeave> info= await askForLeaveService.GetAllAsync().Include(m=>m.User).Include(m=>m.Department).Where(m => m.UserId == userGuid).ToListAsync();
                List<AskLeaveInfoDto>list= info.Select(m =>
                    new AskLeaveInfoDto()
                    {
                        UserId = m.UserId,
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveState = Enum.GetName(typeof(AskState), m.LeaveState),
                        ApproveTime = m.CreateTime
                    }).ToList();
                return list;
            }
        }

        public async Task RemoveAsk(Guid id)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                var ask =await askForLeaveService.GetOneByIdAsync(id);
                ask.LeaveState = (int) AskState.已撤销;
                await askForLeaveService.EditAsync(ask);
            }
        }

        public async Task<List<AskLeaveInfoDto>> GetAllAskByDep(Guid depGuid)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                List<AskForLeave> info = await askForLeaveService.GetAllAsync().Include(m => m.User).Include(m => m.Department).Where(m => m.DepId ==depGuid&&m.LeaveState==(int)AskState.部门主管审批).ToListAsync();
                List<AskLeaveInfoDto> list = info.Select(m =>
                    new AskLeaveInfoDto()
                    {
                        UserId = m.UserId,
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveState = Enum.GetName(typeof(AskState), m.LeaveState),
                        ApproveTime = m.CreateTime
                    }).ToList();
                return list;
            }
        }

        public async Task<AskLeaveInfoDto> GetOneById(Guid id)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                AskForLeave info= await askForLeaveService.GetAllAsync().Include(m => m.User).Include(m => m.Department).Where(m => m.Id == id).FirstAsync();
                AskLeaveInfoDto list = new AskLeaveInfoDto()
                    {
                        Id = info.Id,
                        UserId = info.UserId,
                        UserName = info.User.Name,
                        Department = info.Department.Depname,
                        LeaveSort = info.LeaveSort,
                        LeaveReason = info.LeaveReason,
                        StartTime = info.StartTime,
                        EndTime = info.EndTime,
                        LeaveState = Enum.GetName(typeof(AskState), info.LeaveState),
                        ApproveTime = info.CreateTime
                    };
                return list;
            }
            
        }

        public async Task EditAsk(Guid id, Guid userRightId,bool pass ,string leaveNotReason)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                AskForLeave ask = await askForLeaveService.GetOneByIdAsync(id);
                if (pass)
                {
                    //人事
                    if (userRightId == Guid.Parse("c487756a-10c5-8c77-d783-0f10ddf0837c"))
                    {
                        ask.LeaveState = (int) AskState.审批通过;
                        ask.LeaveNotReason = leaveNotReason;
                    }
                    else
                    {
                        ask.LeaveState = (int) AskState.人事审批;
                        ask.LeaveNotReason = leaveNotReason;
                    }
                }
                else
                {
                    ask.LeaveState = (int) AskState.审批不通过;
                    ask.LeaveNotReason = leaveNotReason;
                }

                await  askForLeaveService.EditAsync(ask);

            }
        }

        public async Task<List<AskLeaveInfoDto>> GetAllAsk()
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                List<AskForLeave> info = await askForLeaveService.GetAllAsync().Include(m => m.User).Include(m => m.Department).Where(m => m.LeaveState == (int)AskState.人事审批).ToListAsync();
                List<AskLeaveInfoDto> list = info.Select(m =>
                    new AskLeaveInfoDto()
                    {
                        UserId = m.UserId,
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveState = Enum.GetName(typeof(AskState), m.LeaveState),
                        ApproveTime = m.CreateTime,
                        StartTime=m.StartTime,
                        EndTime=m.EndTime
                    }).ToList();
                return list;
            }
        }

        public  async Task<List<AskLeaveInfoDto>> GetAskHistory()
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                List<AskForLeave> info = await askForLeaveService.GetAllByOrderAsync(false).Include(m => m.User).Include(m => m.Department).ToListAsync();
                List<AskLeaveInfoDto> list = info.Select(m =>
                    new AskLeaveInfoDto()
                    {
                        UserId = m.UserId,
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveState = Enum.GetName(typeof(AskState), m.LeaveState),
                        ApproveTime = m.CreateTime,
                        StartTime=m.StartTime,
                        EndTime=m.EndTime
                    }).ToList();
                return list;
            }
        }

        public async Task<List<AskLeaveInfoDto>> GetAskHistoryByDepId(Guid depGuid)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                List<AskForLeave> info = await askForLeaveService.GetAllAsync().Include(m => m.User).Include(m => m.Department).Where(m=>m.DepId==depGuid).ToListAsync();
                List<AskLeaveInfoDto> list = info.Select(m =>
                    new AskLeaveInfoDto()
                    {
                        UserId = m.UserId,
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveState = Enum.GetName(typeof(AskState), m.LeaveState),
                        ApproveTime = m.CreateTime
                    }).ToList();
                return list;
            }
        }

    }
}

