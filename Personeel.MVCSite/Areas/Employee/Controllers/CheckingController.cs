using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Models.CheckingViewModels;

namespace Personeel.MVCSite.Areas.Employee.Controllers
{
    public class CheckingController : Controller
    {
        public ICheckingManager checkingManager = new CheckingManager();
        // GET: Employee/Checking
        public async Task<ActionResult> Index()
        {
            var userId = Session["userId"] == null ? Request.Cookies["userId"].Value : Session["userId"].ToString();
            List<CheckingInfoDto> info= await checkingManager.GetCheckingByUserId(Guid.Parse(userId));
            List<CheckingViewModel> list = new List<CheckingViewModel>();
            foreach (var item in info)
            {
                CheckingViewModel temp = new CheckingViewModel()
                {
                    Id = item.Id,
                    BeginTime = item.BeginTime,
                    DayTime = item.DayTime,
                    EndTime = item.EndTime,
                    UserName = item.UserName,
                    Status = item.Status
                };
                list.Add(temp);
            }
            return View(list);
        }

        public async Task<ActionResult> Start()
        {
            
                var userId = Session["userId"] == null ? Request.Cookies["userId"].Value : Session["userId"].ToString();
                await checkingManager.Begin(Guid.Parse(userId));
                TempData["message"] = "上班打卡成功";
                return RedirectToAction("Index");
        }

        public async Task<ActionResult> End()
        {
            
                var userId = Session["userId"] == null ? Request.Cookies["userId"].Value : Session["userId"].ToString();
                await checkingManager.End(Guid.Parse(userId));
                TempData["message"] = "下班打卡成功";
                return RedirectToAction("Index");
        }
    }
}