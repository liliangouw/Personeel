using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.MVCSite.Models.UserViewModels;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public async Task<ActionResult> Index()
        {
            IBLL.IUserManager userManager = new UserManager();
            List<DTO.UserInfoDto>userList = await userManager.GetAllUser();
            List<UserListViewModel> userListViewModels = new List<UserListViewModel>();
            foreach (var item in userList)
            {
                UserListViewModel tempModel = new UserListViewModel()
                {
                    UserNum = item.UserNum,
                    Name = item.Name,
                    Position = item.Position,
                    Department = item.Department,
                    Email = item.Email,
                    UserPower = item.UserRight
                };
                userListViewModels.Add(tempModel);
            }
            return View(userListViewModels);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(Models.UserViewModels.AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                int right;
                if (model.Right == "系统管理员")
                {
                    right = 0;
                }
                else if (model.Right == "人事管理员")
                {
                    right = 1;
                }
                else
                {
                    right = 2;
                }
                IBLL.IUserManager userManager = new UserManager();
                await userManager.AddUser(model.Email, model.Password, model.Name, right, model.BasicMoney);
                return Content("注册成功");
            }
            return View(model);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
