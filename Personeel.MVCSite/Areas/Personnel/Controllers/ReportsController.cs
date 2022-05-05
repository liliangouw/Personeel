using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.EventViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class ReportsController : Controller
    {
        public IAskForLeaveManager askForLeaveManager = new AskForLeaveManager();
        // GET: Personnel/Reports
        public async Task <ActionResult> LeaveReport()
        {
           
            List<EventIndexViewModel> list = new List<EventIndexViewModel>();
            List<DTO.AskLeaveInfoDto> info = await askForLeaveManager.GetAskHistory();
            list = info.Select(m => new EventIndexViewModel()
            {
                UserName = m.UserName,
                ApproveTime = m.ApproveTime,
                StartTime=m.StartTime,
                Department=m.Department,
                EndTime=m.EndTime
            }).ToList();
            return View(list);
           
        }
    }
}