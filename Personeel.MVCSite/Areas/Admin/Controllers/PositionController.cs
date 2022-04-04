using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    public class PositionController : Controller
    {
        // GET: Admin/Position
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Position/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult AddPosition()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPosition(Models.PositionViewModels.AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IPositionManager positionManager = new PositionManager();
                await positionManager.AddPosition(model.PosName, model.PosDescribe);
                return Content("添加职位成功");
            }
            return View(model);
        }

        // GET: Admin/Position/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Position/Edit/5
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

        // GET: Admin/Position/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Position/Delete/5
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
