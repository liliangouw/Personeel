using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Models.EventViewModels;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class EventApprovalController : Controller
    {
        public IEventManager EventManager = new EventManager();
        // GET: Personnel/LeaveApproval
        public async Task<ActionResult> Index(string userRight, string userId)
        {
            ViewBag.userRight = userRight;
            ViewBag.userId = userId;
            List<EventIndexViewModel> list = new List<EventIndexViewModel>();
            //人事
            if (userRight == "c487756a-10c5-8c77-d783-0f10ddf0837c")
            {
                List<DTO.EventInfoDto> info = await EventManager.GetAllEvent();
                list = info.Select(m => new EventIndexViewModel()
                {
                    UserId = m.UserId,
                    Id = m.Id,
                    UserName = m.UserName,
                    EventSort = m.EventSort,
                    ApproveTime = m.ApproveTime,
                    EventState = m.EventState,
                    userPower = userRight
                }).ToList();
            }
            //部门主管
            else if (userRight == "f740cd62-7161-d9b3-4fe7-4d63caa4a143")
            {
                IUserManager userManager = new UserManager();
                UserInfoDto userInfo = await userManager.GetUserById(Guid.Parse(userId));
                Guid depIGuid = userInfo.DepGuid;
                List<DTO.EventInfoDto> info = await EventManager.GetAllEventByDep(depIGuid);
                list = info.Select(m => new EventIndexViewModel()
                {
                    UserId = m.UserId,
                    Id = m.Id,
                    UserName = m.UserName,
                    EventSort = m.EventSort,
                    ApproveTime = m.ApproveTime,
                    EventState = m.EventState,
                    userPower = userRight
                }).ToList();
            }
            return View(list);
        }

        // GET: Personnel/LeaveApproval/Details/5
        public async Task<ActionResult> Edit(Guid id, string userRight)
        {
            var info = await EventManager.GetOneById(id);
            EventIndexViewModel list = new EventIndexViewModel()
            {
                UserId = info.UserId,
                Id = info.Id,
                EventState = info.EventState,
                UserName = info.UserName,
                EventSort = info.EventSort,
                EventReason = info.EventReason,
                Department = info.Department,
                ApproveTime = info.ApproveTime,
                EventNotReason = info.EventNotReason,
                StartTime = info.StartTime,
                EndTime = info.EndTime,
                userPower = userRight
            };
            return View(list);
        }

        // POST: Personnel/LeaveApproval/Delete/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, EventIndexViewModel model)
        {

            await EventManager.EditEvent(id,model.UserId, Guid.Parse(model.userPower), model.IsPass,model.EventNotReason);
            return RedirectToAction("Index", new { userRight = model.userPower, userId = model.UserId });

        }

        public async Task<ActionResult> History(string userRight, string userId)
        {
            ViewBag.userRight = userRight;
            ViewBag.userId = userId;
            List<EventIndexViewModel> list = new List<EventIndexViewModel>();
            //人事
            if (userRight == "c487756a-10c5-8c77-d783-0f10ddf0837c")
            {
                List<DTO.EventInfoDto> info = await EventManager.GetEventHistory();
                list = info.Select(m => new EventIndexViewModel()
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    EventSort = m.EventSort,
                    ApproveTime = m.ApproveTime,
                    EventState = m.EventState,
                    userPower = userRight
                }).ToList();
            }
            //部门主管
            else if (userRight == "aeaeb207-2c3b-7e1d-265a-0ef856ce3871")
            {
                IUserManager userManager = new UserManager();
                UserInfoDto userInfo = await userManager.GetUserById(Guid.Parse(userId));
                Guid depIGuid = userInfo.DepGuid;
                List<DTO.EventInfoDto> info = await EventManager.GetEventHistoryByDepId(depIGuid);
                list = info.Select(m => new EventIndexViewModel()
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    EventSort = m.EventSort,
                    ApproveTime = m.ApproveTime,
                    EventState = m.EventState,
                    userPower = userRight
                }).ToList();
            }
            return View(list);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var info = await EventManager.GetOneById(id);
            EventIndexViewModel list = new EventIndexViewModel()
            {
                UserId = info.UserId,
                Id = info.Id,
                EventState = info.EventState,
                UserName = info.UserName,
                EventSort = info.EventSort,
                EventReason = info.EventReason,
                Department = info.Department,
                ApproveTime = info.ApproveTime,
                EventNotReason = info.EventNotReason,
                StartTime = info.StartTime,
                EndTime = info.EndTime
            };
            return View(list);
        }
    }
}
