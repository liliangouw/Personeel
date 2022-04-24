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
    public class LeaveApprovalController : Controller
    {
        public IAskForLeaveManager askForLeaveManager = new AskForLeaveManager();

        // GET: Personnel/LeaveApproval
        public async Task<ActionResult> Index(string userRight,string userId)
        {
            ViewBag.userRight = userRight;
            ViewBag.userId = userId;
            List<EventIndexViewModel> list = new List<EventIndexViewModel>();
            //人事
            if (userRight == "c487756a-10c5-8c77-d783-0f10ddf0837c")
            {
                List<DTO.AskLeaveInfoDto> info = await askForLeaveManager.GetAllAsk();
                 list = info.Select(m => new EventIndexViewModel()
                {
                    UserId = m.UserId,
                    Id = m.Id,
                    UserName = m.UserName,
                    EventSort = m.LeaveSort,
                    ApproveTime = m.ApproveTime,
                    EventState = m.LeaveState,
                    userPower = userRight
                }).ToList();
            }
            //部门主管
            else if(userRight== "aeaeb207-2c3b-7e1d-265a-0ef856ce3871")
            {
                IUserManager userManager = new UserManager();
                UserInfoDto userInfo=await userManager.GetUserById(Guid.Parse(userId));
                Guid depIGuid = userInfo.DepGuid;
                List<DTO.AskLeaveInfoDto> info = await askForLeaveManager.GetAllAskByDep(depIGuid);
                 list= info.Select(m => new EventIndexViewModel()
                {
                    UserId = m.UserId,
                    Id = m.Id,
                    UserName = m.UserName,
                    EventSort = m.LeaveSort,
                    ApproveTime = m.ApproveTime,
                    EventState = m.LeaveState,
                    userPower = userRight
                }).ToList();
            }
            return View(list);
        }

        // GET: Personnel/LeaveApproval/Details/5
        public async Task<ActionResult> Edit(Guid id,string userRight)
        {
            var info = await askForLeaveManager.GetOneById(id);
            EventIndexViewModel list = new EventIndexViewModel()
            {
                UserId = info.UserId,
                Id = info.Id,
                EventState = info.LeaveState,
                UserName = info.UserName,
                EventSort = info.LeaveSort,
                EventReason = info.LeaveReason,
                Department = info.Department,
                ApproveTime = info.ApproveTime,
                EventNotReason = info.LeaveNotReason,
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
            
               await  askForLeaveManager.EditAsk(id,Guid.Parse(model.userPower), model.IsPass, model.EventNotReason);
               return RedirectToAction("Index",new{ userRight = model.userPower,userId = model.UserId });
            
        }

        public async Task<ActionResult> History(string userRight, string userId)
        {
            ViewBag.userRight = userRight;
            ViewBag.userId = userId;
            List<EventIndexViewModel> list = new List<EventIndexViewModel>();
            //人事
            if (userRight == "c487756a-10c5-8c77-d783-0f10ddf0837c")
            {
                List<DTO.AskLeaveInfoDto> info = await askForLeaveManager.GetAskHistory();
                list = info.Select(m => new EventIndexViewModel()
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    EventSort = m.LeaveSort,
                    ApproveTime = m.ApproveTime,
                    EventState = m.LeaveState,
                    userPower = userRight
                }).ToList();
            }
            //部门主管
            else if (userRight == "aeaeb207-2c3b-7e1d-265a-0ef856ce3871")
            {
                IUserManager userManager = new UserManager();
                UserInfoDto userInfo = await userManager.GetUserById(Guid.Parse(userId));
                Guid depIGuid = userInfo.DepGuid;
                List<DTO.AskLeaveInfoDto> info = await askForLeaveManager.GetAskHistoryByDepId(depIGuid);
                list = info.Select(m => new EventIndexViewModel()
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    EventSort = m.LeaveSort,
                    ApproveTime = m.ApproveTime,
                    EventState = m.LeaveState,
                    userPower = userRight
                }).ToList();
            }
            return View(list);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var info = await askForLeaveManager.GetOneById(id);
            EventIndexViewModel list = new EventIndexViewModel()
            {
                UserId = info.UserId,
                Id = info.Id,
                EventState = info.LeaveState,
                UserName = info.UserName,
                EventSort = info.LeaveSort,
                EventReason = info.LeaveReason,
                Department = info.Department,
                ApproveTime = info.ApproveTime,
                EventNotReason = info.LeaveNotReason,
                StartTime = info.StartTime,
                EndTime = info.EndTime
            };
            return View(list);
        }
    }
}
