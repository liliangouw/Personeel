using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class EncourageOrChastisementController : Controller
    {
        // GET: Personnel/EncourageOrChastisement
        public ActionResult Index()
        {
            return View();
        }

        // GET: Personnel/EncourageOrChastisement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Personnel/EncourageOrChastisement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personnel/EncourageOrChastisement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personnel/EncourageOrChastisement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Personnel/EncourageOrChastisement/Edit/5
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

        // GET: Personnel/EncourageOrChastisement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Personnel/EncourageOrChastisement/Delete/5
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
