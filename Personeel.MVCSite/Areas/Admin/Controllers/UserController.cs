using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
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
                Name = userInfo.Name,
                Gender = userInfo.Gender,
                Department = userInfo.Department,
                Position = userInfo.Position,
                Phone = userInfo.Phone,
                UserNum = userInfo.UserNum,
                UserPower = userInfo.UserPower,
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
                PositionInfoDto positionInfo = null;
                DepInfoDto depInfo = null;
                try
                {
                     positionInfo=await positionManager.GetInfoByName(model.Position);
                     depInfo = await departmentManager.GetInfoByName(model.Department);
                }
                catch
                {
                    return Content("<script >alert('您输入的部门或职位信息有误,请重新输入');</script >", "text/html");
                }
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
                Name = userInfo.Name,
                Gender = userInfo.Gender,
                Department = userInfo.Department,
                Position = userInfo.Position,
                Phone = userInfo.Phone,
                UserNum = userInfo.UserNum,
                UserPower = userInfo.UserPower,
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

        // POST: Admin/User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit( UserListViewModel user)
        {
            try
            {
                IUserManager userManager = new UserManager();
               await userManager.ChangeInfo(user.Email, user.Name, user.Gender, user.Birthday, user.IdNumber, user.Wedlock,
                    user.Race, user.NativePlace, user.Politic, user.Phone, user.TipTopDegree, user.School);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/User/Delete/5
        public async Task<ActionResult> Delete(string email)
        {
            IUserManager userManager = new UserManager();
            UserInfoDto userInfo=await userManager.GetUserByEmail(email);
            UserListViewModel userList = new UserListViewModel()
            {
                Email = userInfo.Email,
                Name = userInfo.Name,
                Gender = userInfo.Gender,
                Department = userInfo.Department,
                Position = userInfo.Position,
                Phone = userInfo.Phone,
                UserNum = userInfo.UserNum,
                UserPower = userInfo.UserPower,
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
            return View(userList);
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string eamil, FormCollection collection)
        {
            try
            { 
                IUserManager userManager = new UserManager(); 
                await  userManager.DeleteUser(eamil);
               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
