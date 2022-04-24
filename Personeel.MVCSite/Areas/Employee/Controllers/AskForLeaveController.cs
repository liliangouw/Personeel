using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.EventViewModels;

namespace Personeel.MVCSite.Areas.Employee.Controllers
{
    public class AskForLeaveController : Controller
    {
        private IAskForLeaveManager askForLeaveManager = new AskForLeaveManager();
        // GET: Employee/AskForLeave
        public async Task<ActionResult> Index(Guid userGuid)
        {
            List<DTO.AskLeaveInfoDto>info=await askForLeaveManager.GetAllAskByUserId(userGuid);
            List<EventIndexViewModel> list = info.Select(m => new EventIndexViewModel()
            {
                Id = m.Id,
                UserName = m.UserName,
                EventSort = m.LeaveSort,
                ApproveTime = m.ApproveTime,
                EventState = m.LeaveState
            }).ToList();
            return View(list);
        }

        // GET: Employee/AskForLeave/Details/5
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

        // GET: Employee/AskForLeave/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/AskForLeave/Create
        [HttpPost]
        public async Task<ActionResult> Create(EventIndexViewModel model)
        {
            try
            {
               await askForLeaveManager.AddAskForLeave(model.UserId, model.EventSort, model.EventReason, model.StartTime,
                    model.EndTime);
                return RedirectToAction("Index",new{userGuid=model.UserId,});
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/AskForLeave/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
           var info= await askForLeaveManager.GetOneById(id);
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

        // POST: Employee/AskForLeave/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, EventIndexViewModel model)
        {
            try
            { 
               await askForLeaveManager.RemoveAsk(id);
               return RedirectToAction("Index", new { userGuid = model.UserId });
            }
            catch
            {
                return View();
            }
        }
    }
}
