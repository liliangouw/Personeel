using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.OperationViewModels;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    public class OperationController : Controller
    {
        // GET: Admin/Operation
        public async Task<ActionResult> Index()
        {
            IOperationManager operationManager = new OperationManager();
            var list=  await operationManager.GetAllOperation();
            List<OperationListViewModel> operationList = new List<OperationListViewModel>();
            foreach (var item in list)
            {
                OperationListViewModel temp = new OperationListViewModel()
                {
                    Id = item.Id,
                    Text = item.Text,
                    CreateTime = item.CreateTime,
                    UserName = item.Name
                };
                operationList.Add(temp);
            }
            return View(operationList);
        }


        // GET: Admin/Operation/Delete/5
        public async Task<ActionResult> Delete(Guid id,string name)
        {
            IOperationManager operationManager = new OperationManager();
            var info=await operationManager.GetOperationById(id,name);
            OperationListViewModel model = new OperationListViewModel()
            {
                Id = info.Id,
                UserName = info.Name,
                CreateTime = info.CreateTime,
                Text = info.Text
            };
            return View(model);
        }

        // POST: Admin/Operation/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                IOperationManager operationManager = new OperationManager();
                operationManager.RemoveOperation(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
