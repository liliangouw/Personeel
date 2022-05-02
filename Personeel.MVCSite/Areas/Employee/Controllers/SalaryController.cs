using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.SalaryViewModels;

namespace Personeel.MVCSite.Areas.Employee.Controllers
{
    public class SalaryController : Controller
    {
        private ISalaryManager salaryManager = new SalaryManager();
        // GET: Employee/Salary
        public async Task<ActionResult> Index(Guid userGuid)
        {
            var info = await salaryManager.GetAllSalaryByUserId(userGuid);
            var list = info.Select(m => new SalaryListViewModel()
            {
                Id = m.Id,
                UserName = m.UserName,
                BasicSalary = m.BasicSalary,
                ActualSalary = m.ActualSalary,
                SalaryDate = m.SalaryDate
            }).ToList();
            return View(list);
        }

        // GET: Employee/Salary/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var info = await salaryManager.GetInfoById(id);
            var list = new AddSalaryViewModel()
            {
                UserId=info.UserId,
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
    }
}
