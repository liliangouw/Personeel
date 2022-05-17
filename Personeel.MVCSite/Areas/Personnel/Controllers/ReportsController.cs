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
                EventSort=m.LeaveSort,
                EventState=m.LeaveState,
                UserName = m.UserName,
                ApproveTime = m.ApproveTime,
                StartTime=m.StartTime,
                Department=m.Department,
                EndTime=m.EndTime
            }).ToList();
            List<int> numberList = new List<int>();

            return View(list);
           
        }

        [HttpPost]
        public async Task<ActionResult> GetData()
        {
            List<EventIndexViewModel> list = new List<EventIndexViewModel>();
            List<DTO.AskLeaveInfoDto> info = await askForLeaveManager.GetAskHistory();
            list = info.Select(m => new EventIndexViewModel()
            {
                EventSort = m.LeaveSort,
                EventState = m.LeaveState,
                UserName = m.UserName,
                ApproveTime = m.ApproveTime,
                StartTime = m.StartTime,
                Department = m.Department,
                EndTime = m.EndTime
            }).ToList();
            List<int> numberList = new List<int>();
           
            //[5, 20, 36, 10, 10, 20]
            numberList.Add(list.Select(m=>m.EventSort=="事假").Count());
            numberList.Add(list.Select(m => m.EventSort == "病假").Count());
            numberList.Add(list.Select(m => m.EventSort == "年假").Count());
            numberList.Add(list.Select(m => m.EventSort == "产假").Count());
            numberList.Add(list.Select(m => m.EventSort == "丧假").Count());
            numberList.Add(list.Select(m => m.EventSort == "其他").Count());
            
            return Json(new { numb = numberList });
        }

        [HttpPost]
        public async Task<ActionResult> GetData2(DateTime startTime, DateTime endTime)
        {
            List<EventIndexViewModel> list = new List<EventIndexViewModel>();
            List<DTO.AskLeaveInfoDto> info = await askForLeaveManager.GetAskHistory();
            list = info.Where(m => m.StartTime <= endTime && m.StartTime >= startTime).Select(m => new EventIndexViewModel()
            {
                EventSort = m.LeaveSort,
                EventState = m.LeaveState,
                UserName = m.UserName,
                ApproveTime = m.ApproveTime,
                StartTime = m.StartTime,
                Department = m.Department,
                EndTime = m.EndTime
            }).ToList();
            List<int> numberList = new List<int>();

            //[5, 20, 36, 10, 10, 20]
            numberList.Add(list.Select(m => m.EventSort == "事假").Count());
            numberList.Add(list.Select(m => m.EventSort == "病假").Count());
            numberList.Add(list.Select(m => m.EventSort == "年假").Count());
            numberList.Add(list.Select(m => m.EventSort == "产假").Count());
            numberList.Add(list.Select(m => m.EventSort == "丧假").Count());
            numberList.Add(list.Select(m => m.EventSort == "其他").Count());

            return Json(new { numb = numberList });
        }


    }
}