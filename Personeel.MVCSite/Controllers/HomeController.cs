using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.MVCSite.Models.UserViewModels;

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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(AddViewModel model)
        {
            IBLL.IUserManager userManager = new UserManager();
            if ( await userManager.Login(model.Email, model.Password))
            {
                DTO.UserInfoDto user=await userManager.GetUserByEmail(model.Email);
                if (user.UserPower == 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else if(user.UserPower==1)
                {
                    
                }
                else
                {
                    
                }
            }
            return View();
        }
    }
}