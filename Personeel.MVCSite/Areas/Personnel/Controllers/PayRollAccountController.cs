using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.SalaryViewModels;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class PayRollAccountController : Controller
    {
        public ISalaryManager salaryManager = new SalaryManager();
        // GET: Personnel/PayRollAccount
        public async Task<ActionResult> Index()
        {
            var info = await salaryManager.GetAllAccount();
            List<AddSalaryViewModel> list = info.Select(m => new AddSalaryViewModel()
            {
                UserId = m.UserId,
                UserName = m.UserName,
                BasicSalary = m.BasicSalary,
                Subsidies = m.Subsidies,
                Accumulationfund = m.Accumulationfund,
                SocialSecurity = m.SocialSecurity,
                Tax = m.Tax
            }).ToList();
            return View(list);
        }

        // GET: Personnel/PayRollAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personnel/PayRollAccount/Create
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

        // GET: Personnel/PayRollAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Personnel/PayRollAccount/Edit/5
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

        // GET: Personnel/PayRollAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Personnel/PayRollAccount/Delete/5
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
