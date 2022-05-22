using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Models.EncourageViewModels;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class EncourageOrChastisementController : Controller
    {
        // GET: Personnel/EncourageOrChastisement
        public async Task<ActionResult> Index(int id = 1, string Name = "")
        {
            IEncourageManager encourageManager = new EncourageManage();
            List<EncourageViewModel> list = new List<EncourageViewModel>();
            List<EncourageInfoDto> info = await encourageManager.GetAll();
            if (Name == "")
            {

            }
            else
            {
                info=info.Where(m => m.UserName.Contains(Name)).ToList();

            }
            foreach (var item in info)
            {
                EncourageViewModel temp = new EncourageViewModel()
                {
                    Category = item.Category,
                    Money = item.Money,
                    Reason = item.Reason,
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Id = item.Id
                };
                list.Add(temp);
            }
            var model = list.ToPagedList(id, 8);
            return View(model);
        }

        // GET: Personnel/EncourageOrChastisement/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            IEncourageManager encourageManager = new EncourageManage();
            var info =await encourageManager.GetOneById(id);
            EncourageViewModel list = new EncourageViewModel()
            {
                Id = info.Id,
                Category = info.Category,
                Money = info.Money,
                Reason = info.Reason,
                UserId = info.UserId,
                UserName = info.UserName
            };
            return View(list);
        }

        // GET: Personnel/EncourageOrChastisement/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            return View();
        }

        // POST: Personnel/EncourageOrChastisement/Create
        [HttpPost]
        public async Task<ActionResult> Create(EncourageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEncourageManager encourageManager = new EncourageManage();
                    await encourageManager.AddEncourage(model.UserId, model.Category, model.Reason, model.Money);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                ViewBag.Users = await new UserManager().GetAllUser();
                return View();
            }
        }

        // GET: Personnel/EncourageOrChastisement/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            IEncourageManager encourageManager = new EncourageManage();
            var info=await encourageManager.GetOneById(id);
            var list = new EncourageViewModel()
            {
                Id = info.Id,
                Category = info.Category,
                Money = info.Money,
                Reason = info.Reason,
                UserId = info.UserId,
                UserName = info.UserName
            };
            return View(list);
        }

        // POST: Personnel/EncourageOrChastisement/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, EncourageViewModel model)
        {
            try
            {
                IEncourageManager encourageManager = new EncourageManage();
                await encourageManager.Edit(id, model.Category, model.Reason, model.Money);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personnel/EncourageOrChastisement/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            IEncourageManager encourageManager = new EncourageManage();
            var info = await encourageManager.GetOneById(id);
            var list = new EncourageViewModel()
            {
                Id = info.Id,
                Category = info.Category,
                Money = info.Money,
                Reason = info.Reason,
                UserId = info.UserId,
                UserName = info.UserName
            };
            return View(list);
        }

        // POST: Personnel/EncourageOrChastisement/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            try
            {
                IEncourageManager encourageManager = new EncourageManage();
                await encourageManager.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
