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
            DateTime endDate,
            Guid departmentGuid)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                await askForLeaveService.CreateAsync(new AskForLeave()
                {
                    ApproveTime = DateTime.Now,
                    DepId = departmentGuid,
                    UserId = userGuid,
                    LeaveSort = leaveSort,
                    LeaveReason = leaveReason,
                    StartTime = startDate,
                    EndTime = endDate,
                    LeaveState = (int)AskState.部门主管审批
                });
            }
        }

        public async Task EditAsk(Guid id, string leaveSort, string leaveReason, DateTime startDate, DateTime endDate)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                var ask = await askForLeaveService.GetOneByIdAsync(id);
                ask.LeaveSort = leaveSort;
                ask.LeaveReason = leaveReason;
                ask.StartTime = startDate;
                ask.EndTime = endDate;
                ask.LeaveState = (int) AskState.部门主管审批;
                await askForLeaveService.EditAsync(ask);
            }
        }

        public async Task<List<AskLeaveInfoDto>> GetAllAskByUserId(Guid userGuid)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                return await askForLeaveService.GetAllAsync().Where(m => m.UserId == userGuid).Select(m =>
                    new AskLeaveInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveState = Enum.GetName(typeof(AskState), m.LeaveState),
                        ApproveTime = m.CreateTime
                    }).ToListAsync();
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
                return await askForLeaveService.GetAllAsync().Where(m => m.DepId == depGuid && m.LeaveState==(int)AskState.部门主管审批).Select(m =>
                    new AskLeaveInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveState = Enum.GetName(typeof(AskState), m.LeaveState),
                        ApproveTime = m.CreateTime
                    }).ToListAsync();
            }
        }

        public async Task<AskLeaveInfoDto> GetOneById(Guid id)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                return await askForLeaveService.GetAllAsync().Where(m => m.Id == id).Select(m =>
                    new AskLeaveInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveReason = m.LeaveReason,
                        LeaveState = Enum.GetName(typeof(AskState),m.LeaveState),
                        LeaveNotReason = m.LeaveNotReason,
                        StartTime = m.StartTime,
                        EndTime = m.EndTime,
                        ApproveTime = m.ApproveTime
                    }).FirstAsync();
            }
            
        }

        public async Task EditAsk(Guid id, int power,bool pass ,string leaveNotReason)
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                AskForLeave ask = await askForLeaveService.GetOneByIdAsync(id);
                if (pass)
                {
                    if (power == 1)
                    {
                        ask.LeaveState = (int) AskState.审批通过;
                        ask.LeaveNotReason = leaveNotReason;
                    }
                    else
                    {
                        ask.LeaveState = (int) AskState.人事审批;
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
                int state = (int) AskState.人事审批;
                return await askForLeaveService.GetAllAsync().Where(m => m.LeaveState == state).Select(m =>
                    new AskLeaveInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        ApproveTime = m.CreateTime
                    }).ToListAsync();
            }
        }

        public  async Task<List<AskLeaveInfoDto>> GetAskHistory()
        {
            using (IAskForLeaveService askForLeaveService = new AskForLeaveService())
            {
                return await askForLeaveService.GetAllAsync().Select(m =>
                    new AskLeaveInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        LeaveSort = m.LeaveSort,
                        LeaveState = Enum.GetName(typeof(AskState), m.LeaveState),
                        ApproveTime = m.CreateTime
                    }).ToListAsync();
            }
        }

    }
}

