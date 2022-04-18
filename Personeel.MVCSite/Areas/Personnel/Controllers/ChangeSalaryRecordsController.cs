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
    public class ChangeSalaryRecordsController : Controller
    {
        // GET: Personnel/ChangeSalaryRecords
        public ISalaryManager salaryManager = new SalaryManager();
        public async Task<ActionResult> Index()
        {
            var info = await salaryManager.GetAllChangeRecord();
            List<ChangeSalaryRecordViewModel> list = info.Select(m => new ChangeSalaryRecordViewModel()
            {
                UserId = m.UserGuid,
                Id = m.Id,
                UserName = m.UserName,
                Reason = m.ChangeReason,
                BeforeSalary = m.BeforeSalary,
                ChangedSalary = m.ChangedSalary,
                ChangeDateTime = m.ChangedDate
            }).ToList();
            return View(list);
        }
    }
}
