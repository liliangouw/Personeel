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
    public class PayRollAccountController : Controller
    {
        public ISalaryManager salaryManager = new SalaryManager();

        public IUserManager UserManager = new UserManager();
        // GET: Personnel/PayRollAccount
        public async Task<ActionResult> Index(int id = 1, string Name = "")
        {
            var userList = await salaryManager.GetAllAccount();
            if (Name == "" )
            {

            }
            else
            {
                userList = userList.Where(m => m.UserName.Contains(Name)).ToList();
            }
            List<AddSalaryViewModel> list = userList.Select(m => new AddSalaryViewModel()
            {
                Id = m.Id,
                UserId = m.UserId,
                UserName = m.UserName,
                BasicSalary = m.BasicSalary,
                Subsidies = m.Subsidies,
                Accumulationfund = m.Accumulationfund,
                SocialSecurity = m.SocialSecurity,
                Tax = m.Tax
            }).ToList();
            var model = list.ToPagedList(id, 8);
            return View(model);
        }

        public async Task<ActionResult> Select()
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            return View();
        }

        [HttpPost]
        public ActionResult Select(SelectUserViewModel model)
        {
            Guid userGuid = model.UserGuid[0];
            return RedirectToAction("Create", new { id = userGuid });
        }

        // GET: Personnel/PayRollAccount/Create
        public async Task<ActionResult> Create(Guid id)
        {
           var info= await UserManager.GetUserById(id);
           AddSalaryViewModel list = new AddSalaryViewModel()
           {
               UserId = info.UserId,
               UserName = info.Name,
               BasicSalary = info.BasicMoney
           };
            return View(list);
        }

        // POST: Personnel/PayRollAccount/Create
        [HttpPost]
        public async Task<ActionResult> Create(AddSalaryViewModel model)
        {
            try
            {
               await salaryManager.AddAccount(model.UserId, model.BasicSalary, model.Subsidies, model.Accumulationfund,
                    model.SocialSecurity, model.Tax);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personnel/PayRollAccount/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
           var info=await salaryManager.GetOneAccountById(id);
           AddSalaryViewModel list = new AddSalaryViewModel()
           {
               Id = info.Id,
               UserId = info.UserId,
               UserName = info.UserName,
               BasicSalary = info.BasicSalary,
               Accumulationfund = info.Accumulationfund,
               SocialSecurity = info.SocialSecurity,
               Subsidies = info.Subsidies,
               Tax = info.Tax
           };
            return View(list);
        }

        // POST: Personnel/PayRollAccount/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, AddSalaryViewModel model)
        {
            try
            {
                await salaryManager.EditAccount(id, model.Subsidies, model.Accumulationfund, model.SocialSecurity, model.Tax);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personnel/PayRollAccount/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var info = await salaryManager.GetOneAccountById(id);
            AddSalaryViewModel list = new AddSalaryViewModel()
            {
                Id = info.Id,
                UserId = info.UserId,
                UserName = info.UserName,
                BasicSalary = info.BasicSalary,
                Accumulationfund = info.Accumulationfund,
                SocialSecurity = info.SocialSecurity,
                Subsidies = info.Subsidies,
                Tax = info.Tax
            };
            return View(list);
        }

        // POST: Personnel/PayRollAccount/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            try
            {
                await salaryManager.RemoveAccount(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
