using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.DepartmentViewModels;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
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
                DepListViewModel tempModel = new DepListViewModel();
                tempModel.DepGuid= item.DepGuid;
                tempModel.DepName = item.DepName;
                tempModel.DepDes = item.DepDes;
                depList.Add(tempModel);
            }
            return View(depList);
        }

        // GET: Admin/Department/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            
            IBLL.IDepartmentManager departmentManager = new DepartmentManager();
            DepListViewModel dep = new DepListViewModel();
            DTO.DepInfoDto depinfo =await departmentManager.GetInfoById(id);
            dep.DepGuid = depinfo.DepGuid;
            dep.DepName = depinfo.DepName;
            dep.DepDes = depinfo.DepDes;
            return View(dep);
        }
        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDepartment(Models.DepartmentViewModels.AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IDepartmentManager departmentManager = new DepartmentManager();
                await departmentManager.AddDep(model.DepName, model.DepDes);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Department/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            IBLL.IDepartmentManager departmentManager = new DepartmentManager();
            DepListViewModel dep = new DepListViewModel();
            DTO.DepInfoDto depinfo = await departmentManager.GetInfoById(id);
            dep.DepGuid = depinfo.DepGuid;
            dep.DepName = depinfo.DepName;
            dep.DepDes = depinfo.DepDes;
            return View(dep);
        }

        // POST: Admin/Department/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, DepListViewModel model)
        {
            try
            {
                IBLL.IDepartmentManager departmentManager = new DepartmentManager();
                departmentManager.EditDep(model.DepName, model.DepDes);
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
