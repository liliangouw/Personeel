using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.SalaryViewModels;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class SalaryController : Controller
    {
        ISalaryManager salaryManager = new SalaryManager();
        // GET: Personnel/Salary
        public async Task<ActionResult> Index(int id = 1, string Name = "")
        {
          var info=  await salaryManager.GetAllSalary();
            if (Name == "")
            {

            }
            else 
            {
                info=info.Where(m => m.UserName.Contains(Name)).ToList();

            }
            var list = info.Select(m => new SalaryListViewModel()
          {
              Id = m.Id,
              UserName = m.UserName,
              BasicSalary = m.BasicSalary,
              ActualSalary = m.ActualSalary,
              SalaryDate = m.SalaryDate
             
          }).ToList();
            var model = list.ToPagedList(id, 8);
          return View(model);
        }

        // GET: Personnel/Salary/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var info =  await salaryManager.GetInfoById(id);
            var list = new AddSalaryViewModel()
            {
                Id = info.Id,
                UserName = info.UserName,
                BasicSalary = info.BasicSalary,
                EncourageOrChastisement = info.EncourageOrChastisement,
                ShouldDays = info.ShouldDays,
                ActualDays = info.ActualDays,
                Accumulationfund = info.Accumulationfund,
                Subsidies = info.Subsidies,
                SocialSecurity = info.SocialSecurity,
                Tax = info.Tax,
                ActualSalary = info.ActualSalary,
                SalaryDate = info.SalaryDate
            };
            return View(list);
        }

        // GET: Personnel/Salary/Create
        public async Task<ActionResult> Select()
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            return View();
        }

        // POST: Personnel/Salary/Create
        [HttpPost]
        public async Task<ActionResult> Select(SelectUserViewModel model)
        {
            await salaryManager.AddSalarys(model.UserGuid);
            return  RedirectToAction("Index");
        }


        // GET: Personnel/Salary/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var info = await salaryManager.GetInfoById(id);
            AddSalaryViewModel list = new AddSalaryViewModel()
            {
                UserName = info.UserName,
                UserId = info.UserId,
                BasicSalary = info.BasicSalary,
                ActualDays = info.ActualDays,
                ShouldDays = info.ShouldDays,
                EncourageOrChastisement = info.EncourageOrChastisement,
                SalaryDate = info.SalaryDate,
                ActualSalary = info.ActualSalary,
                Accumulationfund = info.Accumulationfund,
                Id = id,
                Tax = info.Tax,
                SocialSecurity = info.SocialSecurity,
                Subsidies = info.Subsidies,
            };
            return View(list);
        }

        // POST: Personnel/Salary/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, AddSalaryViewModel model)
        {
            try
            {
               await  salaryManager.EditSalary(id, model.BasicSalary, model.EncourageOrChastisement, model.ShouldDays,
                    model.ActualDays, model.Subsidies, model.Accumulationfund, model.SocialSecurity, model.Tax,
                    model.SalaryDate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personnel/Salary/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var info = await salaryManager.GetInfoById(id);
            AddSalaryViewModel list = new AddSalaryViewModel()
            {
                UserName = info.UserName,
                UserId = id,
                BasicSalary = info.BasicSalary,
                ActualDays = info.ActualDays,
                ShouldDays = info.ShouldDays,
                EncourageOrChastisement = info.EncourageOrChastisement,
                SalaryDate = info.SalaryDate,
                ActualSalary = info.ActualSalary,
                Accumulationfund = info.Accumulationfund,
                Id = info.Id,
                Tax = info.Tax,
                SocialSecurity = info.SocialSecurity,
                Subsidies = info.Subsidies,
            };
            return View(list);
        }

        // POST: Personnel/Salary/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                salaryManager.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
