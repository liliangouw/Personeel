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
        public async Task AddEvent(Guid userGuid, string eventSort, string eventReason)
        {
            using (IEventService eventService = new EventService())
            {
                using (IUserService userService = new UserService())
                {
                    int power = await userService.GetAllAsync().Where(m => m.Id == userGuid)
                        .Select(m => m.UserRight.UserPower).FirstAsync();
                    Guid depGuid = await userService.GetAllAsync().Where(m => m.Id == userGuid)
                        .Select(m => m.DepartmentID).FirstAsync();
                    if (power == 3)
                    {
                        await eventService.CreateAsync(new Event()
                        {
                            DepId = depGuid,
                            UserId = userGuid,
                            EventSort = eventSort, 
                            EventReason=eventReason,
                            IsPassState = (int)AskState.部门主管审批
                        });
                    }
                    else if (power == 2)
                    {
                        await eventService.CreateAsync(new Event()
                        {
                            CreateTime = DateTime.Now,
                            DepId = depGuid,
                            UserId = userGuid,
                            EventSort = eventSort,
                            EventReason = eventReason,
                            IsPassState = (int)AskState.人事审批
                        });
                    }
                }

            }
        }
        //修改申请
        public async Task EditEvent(Guid id, string eventSort, string eventReason)
        {
            int state = (int)AskState.部门主管审批;
            using (IEventService eventService = new EventService())
            {
              var info= await eventService.GetOneByIdAsync(id);
              info.EventSort = eventSort;
              info.EventReason = eventReason;
              info.IsPassState = state;
              await eventService.EditAsync(info);
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
                int state = (int)AskState.人事审批;
                return await eventService.GetAllAsync().Where(m => m.IsPassState == state).Select(m =>
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
                List<Event> info = await eventService.GetAllAsync().Include(m => m.User).Include(m => m.Department)
                    .Where(m => m.DepId ==depGuid).ToListAsync();
                List<EventInfoDto> list = info.Select(m =>
                    new EventInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        EventSort = m.EventSort,
                        EventState = Enum.GetName(typeof(AskState), m.IsPassState),
                        ApproveTime = m.CreateTime
                    }).ToList();
                return list;
            }
        }

        public async Task<List<EventInfoDto>> GetAllEventByUserId(Guid userGuid)
        {
            using (IEventService eventService = new EventService())
            {
                List<Event> info = await eventService.GetAllAsync().Include(m => m.User).Include(m => m.Department)
                    .Where(m => m.UserId == userGuid).ToListAsync();
                List<EventInfoDto> list= info.Select(m =>
                    new EventInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        EventSort = m.EventSort,
                        EventState = Enum.GetName(typeof(AskState), m.IsPassState),
                        ApproveTime = m.CreateTime
                    }).ToList();
                return list;
            }
        }

        public async Task<List<EventInfoDto>> GetEventHistory()
        {
            using (IEventService eventService = new EventService())
            {
                List<Event> info = await eventService.GetAllAsync().Include(m => m.User).Include(m => m.Department).ToListAsync();
                List<EventInfoDto> list = info.Select(m =>
                    new EventInfoDto()
                    {
                        Id = m.Id,
                        UserName = m.User.Name,
                        Department = m.Department.Depname,
                        EventSort = m.EventSort,
                        EventState = Enum.GetName(typeof(AskState), m.IsPassState),
                        ApproveTime = m.CreateTime
                    }).ToList();
                return list;
            }
        }

        public async Task<EventInfoDto> GetOneById(Guid id)
        {
            using (IEventService eventService = new EventService())
            {
                Event info = await eventService.GetAllAsync().Include(m => m.User).Include(m => m.Department).FirstAsync();
                EventInfoDto list = new EventInfoDto()
                    {
                        UserId = info.UserId,
                        Id = info.Id,
                        UserName = info.User.Name,
                        Department = info.Department.Depname,
                        EventSort = info.EventSort,
                        EventReason = info.EventReason,
                        EventNotReason = info.NotReason,
                        EventState = Enum.GetName(typeof(AskState), info.IsPassState),
                        ApproveTime = info.CreateTime
                    };
                return list;
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
