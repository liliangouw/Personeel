using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Filters;
using Personeel.MVCSite.Models.DepartmentViewModels;
using Personeel.MVCSite.Models.PositionViewModels;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    [PersonnelAuth]
    public class PositionController : Controller
    {
        // GET: Admin/Position
        public async Task<ActionResult> Index(int id=1)
        {
            IBLL.IPositionManager positionManager = new PositionManager();
            List<DTO.PositionInfoDto> posInfo = await positionManager.GetInfo();
            List<PosListViewModel> posList = new List<PosListViewModel>();
            foreach (var item in posInfo)
            {
                PosListViewModel tempModel = new PosListViewModel();
                tempModel.PosGuid = item.PositionGuid;
                tempModel.PosName = item.PositionName;
                tempModel.PosDes = item.PositionDescribe;
                posList.Add(tempModel);
            }
            var model = posList.ToPagedList(id, 8);
            return View(model);
        }

        // GET: Admin/Position/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            IBLL.IPositionManager positionManager = new PositionManager();
            PosListViewModel pos = new PosListViewModel();
            DTO.PositionInfoDto posinfo = await positionManager.GetInfoById(id);
            pos.PosGuid = posinfo.PositionGuid;
            pos.PosName = posinfo.PositionName;
            pos.PosDes = posinfo.PositionDescribe;
            return View(pos);
        }

        public ActionResult AddPosition()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPosition(Models.PositionViewModels.AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IPositionManager positionManager = new PositionManager();
                await positionManager.AddPosition(model.PosName, model.PosDescribe);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Position/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            IBLL.IPositionManager positionManager = new PositionManager();
            PosListViewModel pos = new PosListViewModel();
            DTO.PositionInfoDto posinfo = await positionManager.GetInfoById(id);
            pos.PosGuid = posinfo.PositionGuid;
            pos.PosName = posinfo.PositionName;
            pos.PosDes = posinfo.PositionDescribe;
            return View(pos);
        }

        // POST: Admin/Position/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, PosListViewModel posList)
        {
            try
            {
                IBLL.IPositionManager posManager = new PositionManager();
                posManager.ChangeInfo(id,posList.PosName, posList.PosDes);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Position/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            IBLL.IPositionManager positionManager = new PositionManager();
            PosListViewModel pos = new PosListViewModel();
            DTO.PositionInfoDto posinfo = await positionManager.GetInfoById(id);
            pos.PosGuid = posinfo.PositionGuid;
            pos.PosName = posinfo.PositionName;
            pos.PosDes = posinfo.PositionDescribe;
            return View(pos);
        }

        // POST: Admin/Position/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, PosListViewModel posList)
        {
            try
            {
                IBLL.IPositionManager positionManager = new PositionManager();
                positionManager.RemovePosition(id);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
