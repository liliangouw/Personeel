using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Models.UserViewModels;

namespace Personeel.MVCSite.Areas.Employee.Controllers
{
    public class UserController : Controller
    {
        private IUserManager userManager = new UserManager();
        // GET: Employee/User
        public async Task<ActionResult> Index(Guid userGuid)
        {
            UserInfoDto userInfo = await userManager.GetUserById(userGuid);
            UserListViewModel model = new UserListViewModel()
            {
                UserId = userInfo.UserId,
                Email = userInfo.Email,
                Name = userInfo.Name,
                Gender = userInfo.Gender,
                Department = userInfo.Department,
                Position = userInfo.Position,
                Phone = userInfo.Phone,
                UserNum = userInfo.UserNum,
                BasicMoney = userInfo.BasicMoney,
                Birthday = userInfo.Birthday,
                IdNumber = userInfo.IdNumber,
                Politic = userInfo.Politic,
                Wedlock = userInfo.Wedlock,
                Race = userInfo.Race,
                NativePlace = userInfo.NativePlace,
                School = userInfo.School,
                TipTopDegree = userInfo.TipTopDegree,
                BeginWorkDate = userInfo.BeginWorkDate,
                BeFormDate = userInfo.BeFormDate,
                NotWorkDate = userInfo.NotWorkDate
            };
            return View(model);
        }
        
        public async Task<ActionResult> ChangePassword(Guid id)
        {
            UserInfoDto userInfo = await userManager.GetUserById(id);
            UserListViewModel model = new UserListViewModel()
            {
                UserId=userInfo.UserId,
                Email = userInfo.Email,
                Name = userInfo.Name,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(UserListViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PassWord2 == model.PassWord1)
                {
                    var user = await userManager.GetUserById(model.UserId);
                    if (user.PassWord == model.oldPassWord)
                    {
                        await userManager.ChangePassword(model.UserId, model.Email, model.oldPassWord, model.PassWord2);
                        TempData["message"] = "密码重置成功";
                        return RedirectToAction("Index", new { userGuid = model.UserId });
                    }
                    else
                    {
                        TempData["message"] = "旧密码错误！";
                        return View(model);
                    }
                }
                else
                {
                    TempData["message"] = "两次密码输入不一致！";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            
        }
        // GET: Employee/User/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            UserInfoDto userInfo = await userManager.GetUserById(id);
            UserListViewModel model = new UserListViewModel()
            {
                UserId = userInfo.UserId,
                Email = userInfo.Email,
                Name = userInfo.Name,
                Gender = userInfo.Gender,
                Department = userInfo.Department,
                Position = userInfo.Position,
                Phone = userInfo.Phone,
                UserNum = userInfo.UserNum,
                UserPower = userInfo.UserPower.ToString(),
                BasicMoney = userInfo.BasicMoney,
                Birthday = userInfo.Birthday,
                IdNumber = userInfo.IdNumber,
                Politic = userInfo.Politic,
                Wedlock = userInfo.Wedlock,
                Race = userInfo.Race,
                NativePlace = userInfo.NativePlace,
                School = userInfo.School,
                TipTopDegree = userInfo.TipTopDegree,
            };
            return View(model);
        }

        // POST: Employee/User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, UserListViewModel user)
        {
            await userManager.ChangeInfo(user.UserId, user.Email, user.Name, user.Gender, user.Birthday, user.IdNumber, user.Wedlock,
                    user.Race, user.NativePlace, user.Politic, user.Phone, user.TipTopDegree, user.School);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index", new { userGuid = id});
            
        }

        
    }
}
