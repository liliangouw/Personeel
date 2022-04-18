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
    public class ChangeSalaryController : Controller
    {
        public ISalaryManager salaryManager = new SalaryManager();
        // GET: Personnel/ChangeSalary
        public async Task<ActionResult> Index()
        {
            var info= await salaryManager.GetAllUserBasicSalary();
            List<ChangeBasicSalaryViewModel> list = info.Select(m => new ChangeBasicSalaryViewModel()
            {
                UserGuid = m.UserId,
                UserName = m.Name,
                Department = m.Department,
                Position = m.Position,
                BasicSalary = m.BasicMoney
            }).ToList();
            return View(list);
        }

        // GET: Personnel/ChangeSalary/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var m = await salaryManager.GetOneBasicSalary(id);
            ChangeBasicSalaryViewModel list = new ChangeBasicSalaryViewModel()
            {
                UserGuid = m.UserId,
                UserName = m.Name,
                Department = m.Department,
                Position = m.Position,
                BasicSalary = m.BasicMoney
            };
            return View(list);
        }

        // POST: Personnel/ChangeSalary/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, ChangeBasicSalaryViewModel model)
        {
            try
            {
                await salaryManager.EditUserBasicSalary(id,model.Reason ,model.BasicSalary);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
