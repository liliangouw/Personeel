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
        public IUserManager userManager = new UserManager();
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

        public async Task< ActionResult> PersonReprot()
        {
            ViewBag.Department = await new DepartmentManager().GetInfo();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetPerson(DateTime? start = null, DateTime? end = null,string depGuid="")
        {
            start = start ?? new DateTime(2000, 1, 1, 0, 0, 0);
            end = end ?? DateTime.Now;
            var userList = await userManager.GetAllUser();
            userList = userList.Where(m => m.BeginWorkDate >= start && m.BeginWorkDate <= end).ToList();
            if (depGuid != "")
            {
                Guid DepGuid = Guid.Parse(depGuid);
                userList = userList.Where(m => m.DepGuid.Equals(DepGuid)).ToList();
            }
            List<string> gender = new List<string>();
            gender.Add("{value:" + userList.Where(m => m.Gender == "男").Count() + ",name:'男'}");
            gender.Add("{value:" + userList.Where(m => m.Gender == "女").Count() + ",name:'女'}");

            List<string> age = new List<string>();
            DateTime now = DateTime.Now;
            age.Add("{value:" + userList.Where(m =>(now.Year-m.Birthday.Year)>=18&& (now.Year - m.Birthday.Year) <23).Count() + ",name:'18-23岁'}");
            age.Add("{value:" + userList.Where(m => (now.Year-m.Birthday.Year) >= 23 && (now.Year-m.Birthday.Year) < 28).Count() + ",name:'23-28岁'}");
            age.Add("{value:" + userList.Where(m => (now.Year-m.Birthday.Year) >= 28 && (now.Year-m.Birthday.Year) < 33).Count() + ",name:'28-33岁'}");
            age.Add("{value:" + userList.Where(m => (now.Year-m.Birthday.Year) >= 33 && (now.Year - m.Birthday.Year) < 38).Count() + ",name:'33-38岁'}");
            age.Add("{value:" + userList.Where(m => (now.Year - m.Birthday.Year) >= 38).Count() + ",name:'38岁+'}");

            List<string> xueli = new List<string>();
            xueli.Add("{value:" + userList.Where(m =>m.TipTopDegree=="专科").Count() + ",name:'专科'}");
            xueli.Add("{value:" + userList.Where(m => m.TipTopDegree == "本科").Count() + ",name:'本科'}");
            xueli.Add("{value:" + userList.Where(m => m.TipTopDegree == "硕士").Count() + ",name:'硕士'}");

            List<string> wedlock = new List<string>();
            wedlock.Add("{value:" + userList.Where(m => m.Wedlock=="未婚").Count() + ",name:'未婚'}");
            wedlock.Add("{value:" + userList.Where(m => m.Wedlock=="已婚").Count() + ",name:'已婚'}");
            

            return Json(new { Gender = gender,Age=age,Xueli=xueli,Wedlock=wedlock });
        }


    }
}