using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.OperationViewModels;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    public class OperationController : Controller
    {
        // GET: Admin/Operation
        public async Task<ActionResult> Index(int pageIndex=1,int pageSize=12)
        {
            IOperationManager operationManager = new OperationManager();
            //当前第N页数据
            var list=  await operationManager.GetAllOperation(pageIndex-1, pageSize);
            //获取总条数
            var dataCount = await operationManager.GetDataCount();
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
            return View(new PagedList<OperationListViewModel>(operationList, pageIndex, pageSize, dataCount));
        }


        // GET: Admin/Operation/Delete/5
        public async Task<ActionResult> Delete(Guid id,string name)
        {
            IOperationManager operationManager = new OperationManager();
            var info=await operationManager.GetOperationById(id);
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
