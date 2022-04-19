using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.DepartmentViewModels;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class TransferController : Controller
    {
        private IDepartmentManager departmentManager = new DepartmentManager();
        // GET: Personnel/Transfer
        public async Task<ActionResult> Index()
        {
            var info= await departmentManager.GetAllUserDepartment();
            List<UserDepartmentsViewModel> list = info.Select(m => new UserDepartmentsViewModel()
            {
                UserGuid = m.UserId,
                UserDep = m.BeforeDep,
                UserPos = m.BeforePos,
                UserName = m.UserName
            }).ToList();
            return View(list);
        }



        // GET: Personnel/Transfer/Edit/5
        public async Task<ActionResult> Edit(Guid userGuid)
        {
            ViewBag.Departments = await new DepartmentManager().GetInfo();
            ViewBag.Positions = await new PositionManager().GetInfo();
            var info=await departmentManager.GetOneUserDepartment(userGuid);
           TransferRecordViewModel list = new TransferRecordViewModel()
           {
               UserGuid = info.UserId,
               UserName = info.UserName,
               BeforeDep = info.BeforeDep,
               BeforePos = info.BeforePos,
               TransferDep = info.TransferDep,
               TransferPos = info.TransferPos,
               Reason = info.TransferReason
           };
           return View(list);
        }

        // POST: Personnel/Transfer/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid userGuid, TransferRecordViewModel model)
        {
            try
            {
                await departmentManager.CreateTransferInfo(userGuid, model.TransferDep, model.TransferPos, model.Reason);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> RecordIndex()
        {
           var info=await departmentManager.GetAllTransferRecords();
           List<TransferRecordViewModel> list = info.Select(m => new TransferRecordViewModel()
           {
               Id = m.Id,
               UserName = m.UserName,
               BeforeDep = m.BeforeDep,
               BeforePos = m.BeforePos,
               TransferDep = m.TransferDep,
               TransferPos = m.TransferPos,
               Reason = m.TransferReason,
               TransferDateTime = m.TransferDateTime
           }).ToList();
           return View(list);
        }
    }
}
