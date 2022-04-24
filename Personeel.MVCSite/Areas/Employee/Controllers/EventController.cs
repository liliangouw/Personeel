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
    public class EventController : Controller
    {
        private IEventManager eventManager = new EventManager();
        // GET: Employee/Event
        public async Task<ActionResult> Index(Guid userGuid)
        {
            List<DTO.EventInfoDto> info = await eventManager.GetAllEventByUserId(userGuid);
            List<EventIndexViewModel> list = info.Select(m => new EventIndexViewModel()
            {
                Id = m.Id,
                UserName = m.UserName,
                EventSort = m.EventSort,
                ApproveTime = m.ApproveTime,
                EventState = m.EventState
            }).ToList();

            return View(list);
        }

        // GET: Employee/Event/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var info = await eventManager.GetOneById(id);
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

        // GET: Employee/Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Event/Create
        [HttpPost]
        public async Task<ActionResult> Create(EventIndexViewModel model)
        {
            try
            {
               await eventManager.AddEvent(model.UserId, model.EventSort, model.EventReason);

                return RedirectToAction("Index",new{userGuid=model.UserId});
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Event/Edit/

        // GET: Employee/Event/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var info = await eventManager.GetOneById(id);
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

        // POST: Employee/Event/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, EventIndexViewModel model)
        {
            try
            {
                await eventManager.RemoveEvent(id);
                return RedirectToAction("Index",new { userGuid = model.UserId });
            }
            catch
            {
                return View();
            }
        }
    }
}
