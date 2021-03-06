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
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class UserController : Controller
    {
        // GET: Personnel/User
        public async Task<ActionResult> Index(int id = 1, string Name = "", string Department = "")
        {
            IBLL.IUserManager userManager = new UserManager();
            List<DTO.UserInfoDto> userList = await userManager.GetAllUser();
            if (Name == "" && Department == "")
            {

            }
            else if (Name != "" && Department == "")
            {
                userList = userList.Where(m => m.Name.Contains(Name)).ToList();

            }
            else if (Name == "" && Department != "")
            {
                Guid DepGuid = Guid.Parse(Department);
                userList = userList.Where(m => m.DepGuid.Equals(DepGuid)).ToList();
            }
            else
            {
                Guid DepGuid = Guid.Parse(Department);
                userList = userList.Where(m => m.DepGuid.Equals(DepGuid) && m.Name.Contains(Name)).ToList();
            }
            List<UserListViewModel> userListViewModels = new List<UserListViewModel>();
            foreach (var item in userList)
            {
                string temp = "";
                if (item.UserPower == 0)
                {
                    temp = "系统管理员";
                }
                else if (item.UserPower == 1)
                {
                    temp = "人事管理员";
                }
                else if (item.UserPower == 3 & item.IsManager == true)
                {
                    temp = "部门主管";
                }
                else
                {
                    temp = "员工";
                }
                UserListViewModel tempModel = new UserListViewModel()
                {
                    UserId = item.UserId,
                    UserNum = item.UserNum,
                    Name = item.Name,
                    Position = item.Position,
                    Department = item.Department,
                    Email = item.Email,
                    UserPower = temp
                };
                userListViewModels.Add(tempModel);
            }
            var model = userListViewModels.ToPagedList(id, 8);
            ViewBag.Department = await new DepartmentManager().GetInfo();
            return View(model);
        }

        // GET: Admin/User/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            IUserManager userManager = new UserManager();
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
                BeginWorkDate = userInfo.BeginWorkDate,
                BeFormDate = userInfo.BeFormDate,
                NotWorkDate = userInfo.NotWorkDate
            };
            return View(model);
        }

        public async Task<ActionResult> AddUser()
        {
            //下拉框传值
            //IDepartmentManager departmentManager = new DepartmentManager();
            //List<DTO.DepInfoDto> depList = await departmentManager.GetInfo();
            //IPositionManager positionManager = new PositionManager();
            //List<DTO.PositionInfoDto> posList = await positionManager.GetInfo();
            //AddViewModel model = new AddViewModel();
            //model.DepList = depList;
            //model.PosList = posList;
            ViewBag.Departments = await new DepartmentManager().GetInfo();
            ViewBag.Positions = await new PositionManager().GetInfo();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(Models.UserViewModels.AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IUserManager userManager = new UserManager();
                await userManager.AddUser(model.Email, model.Password, model.Name, model.Right, model.BasicMoney, model.Department, model.Position);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/User/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            IUserManager userManager = new UserManager();
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

        // POST: Admin/User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(UserListViewModel user)
        {
           
            IUserManager userManager = new UserManager();
            await userManager.ChangeInfo(user.UserId, user.Email, user.Name, user.Gender, user.Birthday, user.IdNumber, user.Wedlock,
                 user.Race, user.NativePlace, user.Politic, user.Phone, user.TipTopDegree, user.School);
            BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
            return RedirectToAction("Index");

        }
        public async Task<ActionResult> Delete(Guid id)
        {
            IUserManager userManager = new UserManager();
            UserInfoDto userInfo = await userManager.GetUserById(id);
            UserListViewModel userList = new UserListViewModel()
            {
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
            return View(userList);
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {


            IUserManager userManager = new UserManager();
            await userManager.DeleteUser(id);
            BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
            return RedirectToAction("Index");

        }
    }
}