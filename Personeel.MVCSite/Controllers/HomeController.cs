using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.MVCSite.Models.UserViwModels;

namespace Personeel.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(Models.UserViwModels.AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                int right;
                if (model.Right == "系统管理员")
                {
                    right = 0;
                }
                else if(model.Right=="人事管理员")
                {
                    right = 1;
                }
                else
                {
                    right = 2;
                }
                IBLL.IUserManager userManager = new UserManager();
                await userManager.AddUser(model.Email, model.Password, model.Name,right, model.BasicMoney);
                return Content("注册成功");
            }
            return View(model);
        }

        public ActionResult PositionList()
        {
            return View();
        }
        public ActionResult AddPosition()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPosition(Models.PositionViewModels.AddPosition model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IPositionManager ipositionManager = new PositionManager();
                await ipositionManager.AddPosition(model.PosName,model.PosDesc);
                return Content("注册成功");
            }
            return View(model);
        }
    }
}