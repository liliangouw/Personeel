using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.MVCSite.Models.UserViewModels;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

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
                Guid userId;
                string userName;
                if (userManager.Login(model.LoginName, model.LoginPwd, out userId, out userName))
                {
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("userEmail")
                        {
                            Value = model.LoginName,
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie("userId")
                        {
                            Value = userId.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie("userName")
                        {
                            Value = userName,
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["userEmail"] = model.LoginName;
                        Session["userid"] = userId.ToString();
                        Session["userName"] = userName;
                    }
                    BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                    //跳转
                    DTO.UserInfoDto user = await userManager.GetUserByEmail(model.LoginName);
                    if (user.UserPower == 0)
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    else if (user.UserPower == 1)
                    {
                        return RedirectToAction("Index", "Employee", new { area = "Employee" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Personnel", new { area = "PersonnelAdmin" });
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "您的账号密码有误");
            }

            return View();
        }
    }
}
