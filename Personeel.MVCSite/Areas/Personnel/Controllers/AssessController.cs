using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.AssessViewModels;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    
    public class AssessController : Controller
    {
        private IAssessManager assessManager = new AssessManager();
        // GET: Personnel/Assess
        public async Task<ActionResult> Index(int id = 1, string Name = "", string Department = "")
        {
            
            var info=await assessManager.GetAll();
            if (Name == "" && Department == "")
            {

            }
            else if (Name != "" && Department == "")
            {
                info = info.Where(m => m.UserName.Contains(Name)).ToList();

            }
            else if (Name == "" && Department != "")
            {
                info = info.Where(m => m.UserDep.Equals(Department)).ToList();
            }
            else
            {
                info = info.Where(m => m.UserDep.Equals(Department) && m.UserName.Contains(Name)).ToList();
            }
            List<AssessListViewModel> list = info.Select(m => new AssessListViewModel()
            {
                Id = m.Id,
                UserName = m.UserName,
                UserDep = m.UserDep,
                AssessName = m.AssessName,
                AssessType = m.AssessType,
                Result = m.Result,
                CreateTime = m.CreateTime,
                State = m.State
            }).ToList();
            var model = list.ToPagedList(id, 8);
            ViewBag.Department = await new DepartmentManager().GetInfo();
            return View(model);
        }


        // GET: Personnel/Assess/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            return View();
        }

        // POST: Personnel/Assess/Create
        [HttpPost]
        public async Task<ActionResult> Create(AddAssessViewModel model)
        {
            await assessManager.Add(model.AssessName, model.AssessType, model.UserGuid);
            return RedirectToAction("Index");
        }

        // GET: Personnel/Assess/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            
            var info =await assessManager.GetOneById(id);
            EditAssess list = new EditAssess()
            {
                AssessName = info.AssessName,
                AssessDep = info.UserDep,
                UserName = info.UserName,
                AssessType = info.AssessType
            };
            return View(list);
        }

        // POST: Personnel/Assess/Edit/5
        [HttpPost]
        public async Task<ActionResult>Edit(EditAssess model)
        {
            
            await assessManager.Edit(model.Id, model.Result, 1);
            return RedirectToAction("Index");
        }

        // GET: Personnel/Assess/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var info = await assessManager.GetOneById(id);
            AssessListViewModel list = new AssessListViewModel()
            {
                AssessName = info.AssessName,
                UserDep = info.UserDep,
                UserName = info.UserName,
                AssessType = info.AssessType,
                CreateTime = info.CreateTime,
                Id = info.Id,
                Result = info.Result,
                State = info.State
            };
            return View(list);
        }

        // POST: Personnel/Assess/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            await assessManager.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
