using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Admin/Department
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Department/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

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
                return Content("添加部门成功");
            }
            return View(model);
        }

        // GET: Admin/Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Department/Edit/5
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

        // GET: Admin/Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Department/Delete/5
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
