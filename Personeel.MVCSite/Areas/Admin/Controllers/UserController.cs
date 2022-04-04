using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
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
                    UserPower = item.UserPower
                };
                userListViewModels.Add(tempModel);
            }
            return View(userListViewModels);
        }

        // GET: Admin/User/Details/5
        public async Task<ActionResult> Details(string email)
        {
            IUserManager userManager = new UserManager();
            UserInfoDto userInfo=await userManager.GetUserByEmail(email);
            UserListViewModel model = new UserListViewModel()
            {
                Email = userInfo.Email,
                Department = userInfo.Department,
                Name = userInfo.Name,
                Position = userInfo.Position,
                UserNum = userInfo.UserNum,
                UserPower = userInfo.UserPower
            };
            return View(model);
        }

        public ActionResult AddUser()
        {
            //下拉框传值
            //IDepartmentManager departmentManager = new DepartmentManager();
            //List<DTO.DepInfoDto> depList = await departmentManager.GetInfo();
            //IPositionManager positionManager = new PositionManager();
            //List<DTO.PositionInfoDto> posList = await positionManager.GetInfo();
            //AddViewModel model = new AddViewModel();
            //model.DepList = depList;
            //model.PosList = posList;
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

                IBLL.IPositionManager positionManager = new PositionManager();
                IBLL.IDepartmentManager departmentManager = new DepartmentManager();
                DTO.PositionInfoDto positionInfo=await positionManager.GetInfoByName(model.Position);
                DTO.DepInfoDto depInfo = await departmentManager.GetInfoByName(model.Department);
                IBLL.IUserManager userManager = new UserManager();
                await userManager.AddUser(model.Email, model.Password, model.Name, right, model.BasicMoney,depInfo.DepGuid,positionInfo.PositionGuid);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/User/Edit/5
        public async Task<ActionResult> Edit(string email)
        {
            IUserManager userManager = new UserManager();
            UserInfoDto userInfo = await userManager.GetUserByEmail(email);
            UserListViewModel model = new UserListViewModel()
            {
                Email = userInfo.Email,
                Department = userInfo.Department,
                Name = userInfo.Name,
                Position = userInfo.Position,
                UserNum = userInfo.UserNum,
                UserPower = userInfo.UserPower
            };
            return View(model);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(string email, UserListViewModel userListViewModel)
        {
            try
            {

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
