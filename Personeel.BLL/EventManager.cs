using System;
using System.Collections.Generic;
using System.Data;
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
    public class EventManager : IEventManager
    {
        public async Task AddEvent(Guid userGuid, string eventSort, string eventReason, Guid departmentGuid)
        {
            using (IEventService eventService = new EventService())
            {
                await eventService.CreateAsync(new Event()
                {
                    DepId = departmentGuid,
                    UserId = userGuid,
                    EventSort = eventSort,
                    EventReason = eventReason
                });
            }
        }
        //修改申请
        public async Task EditEvent(Guid id, string eventSort, string eventReason)
        {
            using (IEventService eventService = new EventService())
            {
              var info= await eventService.GetOneByIdAsync(id);
              info.EventSort = eventSort;
              info.EventReason = eventReason;
              info.IsPassState = (int) AskState.部门主管审批;
            }
        }
        //审批
        public async Task EditEvent(Guid id,Guid userGuid, int power, bool pass, string eventSort, string leaveNotReason)
        {
            using (IEventService eventService = new EventService())
            {
                Event info = await eventService.GetOneByIdAsync(id);
                    if (pass)
                    {
                        if (power == 1)
                        {
                            info.IsPassState = (int)AskState.审批通过;
                            info.NotReason = leaveNotReason;
                        }
                        else
                        {
                            info.IsPassState = (int)AskState.人事审批;
                        }
                    }
                    else
                    {
                        info.IsPassState = (int)AskState.审批不通过;
                        info.NotReason = leaveNotReason;
                    }

                    await eventService.EditAsync(info);
            }
        }

        public async Task<List<EventInfoDto>> GetAllEvent()
        {
            using (IEventService eventService = new EventService())
            {
              return await eventService.GetAllAsync().Where(m => m.IsPassState == (int) AskState.人事审批).Select(m =>
                    new EventInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        EventSort = m.EventSort,
                        ApproveTime = m.CreateTime
                    }).ToListAsync();
            }
        }

        public async Task<List<EventInfoDto>> GetAllEventByDep(Guid depGuid)
        {
            using (IEventService eventService = new EventService())
            {
                return await eventService.GetAllAsync().Where(m => m.DepId == depGuid).Select(m =>
                    new EventInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        EventSort = m.EventSort,
                        ApproveTime = m.CreateTime
                    }).ToListAsync();
            }
        }

        public async Task<List<EventInfoDto>> GetAllEventByUserId(Guid userGuid)
        {
            using (IEventService eventService = new EventService())
            {
                return await eventService.GetAllAsync().Where(m => m.UserId ==userGuid).Select(m =>
                    new EventInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        EventSort = m.EventSort,
                        ApproveTime = m.CreateTime
                    }).ToListAsync();
            }
        }

        public async Task<List<EventInfoDto>> GetEventHistory()
        {
            using (IEventService eventService = new EventService())
            {
                return await eventService.GetAllAsync().Select(m =>
                    new EventInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        EventSort = m.EventSort,
                        ApproveTime = m.CreateTime
                    }).ToListAsync();
            }
        }

        public async Task<EventInfoDto> GetOneById(Guid id)
        {
            using (IEventService eventService = new EventService())
            {
                return await eventService.GetAllAsync().Where(m=>m.Id==id).Select(m =>
                    new EventInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        EventSort = m.EventSort,
                        EventReason = m.EventReason,
                        EventNotReason = m.NotReason,
                        EventState = Enum.GetName(typeof(AskState), m.IsPassState),
                        ApproveTime = m.CreateTime
                    }).FirstAsync();
            }
        }

        public async Task RemoveEvent(Guid id)
        {
            using (IEventService eventService = new EventService())
            {
                var ask = await eventService.GetOneByIdAsync(id);
                ask.IsPassState = (int)AskState.已撤销;
                await eventService.EditAsync(ask);
            }
        }
    }
}
