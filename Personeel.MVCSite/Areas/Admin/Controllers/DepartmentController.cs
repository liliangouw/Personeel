using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Filters;
using Personeel.MVCSite.Models.DepartmentViewModels;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    [PersonnelAuth]
    public class DepartmentController : Controller
    {
        // GET: Admin/Department
        public async Task<ActionResult> Index()
        {
            IBLL.IDepartmentManager departmentManager = new DepartmentManager();
            List<DTO.DepInfoDto> depInfo = await departmentManager.GetInfo();
            List<DepListViewModel> depList = new List<DepListViewModel>();
            foreach (var item in depInfo)
            {
                IBLL.IUserManager userManager = new UserManager();
                 UserInfoDto user = await userManager.GetUserById(item.DepUserGuid);
                DepListViewModel tempModel = new DepListViewModel
                {
                    DepGuid = item.DepGuid,
                    DepName = item.DepName,
                    DepDes = item.DepDes,
                    DepUser = user.Name,
                    DepUserId = item.DepUserGuid
                };
                depList.Add(tempModel);
            }
            return View(depList);
        }

        // GET: Admin/Department/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            IBLL.IUserManager userManager = new UserManager();
            IBLL.IDepartmentManager departmentManager = new DepartmentManager();
            DepListViewModel dep = new DepListViewModel();
            DTO.DepInfoDto depinfo =await departmentManager.GetInfoById(id);
            DTO.UserInfoDto User=await userManager.GetUserById(depinfo.DepUserGuid);
            dep.DepGuid = depinfo.DepGuid;
            dep.DepName = depinfo.DepName;
            dep.DepDes = depinfo.DepDes;
            dep.DepUserId = depinfo.DepUserGuid;
            dep.DepUser = User.Name;
             
            return View(dep);
        }
        [HttpGet]
        public async Task<ActionResult> AddDepartment()
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDepartment(Models.DepartmentViewModels.AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                IBLL.IDepartmentManager departmentManager = new DepartmentManager();
                await departmentManager.AddDep(model.DepName, model.DepDes,model.DepUser);
                IUserManager userManager = new UserManager();
                await userManager.ChangePower(model.DepUser, Guid.Parse("aeaeb207-2c3b-7e1d-265a-0ef856ce3871"));
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Department/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            IBLL.IDepartmentManager departmentManager = new DepartmentManager();
            DepListViewModel dep = new DepListViewModel();
            DTO.DepInfoDto depinfo = await departmentManager.GetInfoById(id);
            dep.DepGuid = depinfo.DepGuid;
            dep.DepName = depinfo.DepName;
            dep.DepDes = depinfo.DepDes;
            dep.DepUserId = depinfo.DepUserGuid;
            return View(dep);
        }

        // POST: Admin/Department/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, DepListViewModel model)
        {
            try
            {
                IBLL.IDepartmentManager departmentManager = new DepartmentManager();
                DepInfoDto info=await departmentManager.GetInfoById(id);
                IUserManager userManager = new UserManager();
                await userManager.ChangePower(info.DepUserGuid, Guid.Parse("f740cd62-7161-d9b3-4fe7-4d63caa4a143"));
                await userManager.ChangePower(model.DepUserId, Guid.Parse("aeaeb207-2c3b-7e1d-265a-0ef856ce3871"));
                await departmentManager.EditDep(model.DepName, model.DepDes,model.DepUserId);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString()+":"+Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Department/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            IBLL.IDepartmentManager departmentManager = new DepartmentManager();
            DepListViewModel dep = new DepListViewModel();
            DTO.DepInfoDto depinfo = await departmentManager.GetInfoById(id);
            dep.DepGuid = depinfo.DepGuid;
            dep.DepName = depinfo.DepName;
            dep.DepDes = depinfo.DepDes;
            return View(dep);
        }

        // POST: Admin/Department/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id,DepListViewModel depList)
        {
            try
            {
                IDepartmentManager departmentManager = new DepartmentManager();
                departmentManager.RemoveDep(id);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
