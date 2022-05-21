using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.EventViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class ReportsController : Controller
    {
        public IAskForLeaveManager askForLeaveManager = new AskForLeaveManager();
        // GET: Personnel/Reports
        public async Task <ActionResult> LeaveReport(int id=1)
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
            var model = list.ToPagedList(id, 4);

            return View(model);
           
        }

        [HttpPost]
        public async Task<ActionResult> GetData(DateTime? start = null, DateTime? end = null)
        {
            start = start ?? new DateTime(2000, 1, 1, 0, 0, 0);
            end = end ?? DateTime.Now;
            List<EventIndexViewModel> list = new List<EventIndexViewModel>();
            List<DTO.AskLeaveInfoDto> info = await askForLeaveManager.GetAskHistory();
            info=info.Where(m => m.ApproveTime >= start && m.ApproveTime <= end).ToList();
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
            List<string> stateList = new List<string>();

            numberList.Add(list.Where(m => m.EventSort == "事假").Count());
            numberList.Add(list.Where(m => m.EventSort == "病假").Count());
            numberList.Add(list.Where(m => m.EventSort == "年假").Count());
            numberList.Add(list.Where(m => m.EventSort == "产假").Count());
            numberList.Add(list.Where(m => m.EventSort == "丧假").Count());
            numberList.Add(list.Where(m => m.EventSort == "其他").Count());

            string v1 = "{value:" + list.Where(m => m.EventState == "部门主管审批").Count() + ",name:'部门主管审批'}";
            string v2 = "{value:" + list.Where(m => m.EventState == "人事审批").Count() + ",name:'人事审批'}";
            string v3 = "{value:" + list.Where(m => m.EventState == "审批通过").Count() + ",name:'通过'}";
            string v4 = "{value:" + list.Where(m => m.EventState == "审批不通过").Count() + ",name:'不通过'}";
            string v5 = "{value:" + list.Where(m => m.EventState == "已撤销").Count() + ",name:'已撤销'}";
            stateList.Add(v1);
            stateList.Add(v2);
            stateList.Add(v3);
            stateList.Add(v4);
            stateList.Add(v5);
            return Json(new {numb = numberList,state=stateList});
        }




    }
}