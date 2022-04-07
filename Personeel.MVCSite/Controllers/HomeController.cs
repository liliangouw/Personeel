using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.UserViewModels;

namespace Personeel.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               IBLL.IUserManager userManager = new UserManager();
                if ( await userManager.Login(model.LoginName, model.LoginPwd))
                {
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.LoginName,
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["loginName"] = model.LoginName;
                    }
                    //跳转
                    DTO.UserInfoDto user=await userManager.GetUserByEmail(model.LoginName);
                    if (user.UserPower == 0)
                    {
                        return RedirectToAction("Index", "User", new { area = "Admin" });
                    }
                    else if(user.UserPower==1)
                    {
                        return Content("欢迎你人事管理员");
                    }
                    else
                    {
                        return Content("欢迎您普通员工");
                    }

                }
            }
            else
            {
                ModelState.AddModelError("","您的账号密码有误");
            }
            
            return View();
        }
    }
}