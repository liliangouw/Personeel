using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Filters;
using Personeel.MVCSite.Models.ArticleViewModels;
using Personeel.MVCSite.Models.DepartmentViewModels;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    [PersonnelAuth]
    public class RegulatoryController : Controller
    {
        // GET: Admin/Regulatory
        public async Task<ActionResult> Index()
        {
            IRegulatoryManager regulatoryManager = new RegulatoryManager();
            List<DTO.RegulatoryInfoDto> regulatoryInfo = await regulatoryManager.GetAllRegulatory();
            List<ArticleListViewModel> articleList = new List<ArticleListViewModel>();
            foreach (var item in  regulatoryInfo)
            {
                ArticleListViewModel tempModel = new ArticleListViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Des = item.Des,
                    Writer = item.Name,
                    CreateTime = item.CreateTime
                };
                articleList.Add(tempModel);
            }
            return View(articleList);
        }
        [HttpGet]
        public  ActionResult CreateArticle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(CreateArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userid = Guid.Parse(Session["userId"].ToString());
                IBLL.IRegulatoryManager regulatoryManager = new RegulatoryManager();
                regulatoryManager.AddRegulatory(model.Title, model.Des, model.Text, userid);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("","添加失败");
            return View();
        }

        public async Task<ActionResult> Details(Guid id)
        {

            IRegulatoryManager regulatoryManager = new RegulatoryManager();
            DTO.RegulatoryInfoDto item = await regulatoryManager.GetOneRegulatory(id);
            ArticleListViewModel articleDetail = new ArticleListViewModel()
            {
                Id = item.Id,
                Title = item.Title,
                Des = item.Des,
                Text = item.Text,
                Writer = item.Name,
                CreateTime = item.CreateTime
            };
            return View(articleDetail);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            IRegulatoryManager regulatoryManager = new RegulatoryManager();
            DTO.RegulatoryInfoDto item = await regulatoryManager.GetOneRegulatory(id);
            ArticleListViewModel  articleList = new ArticleListViewModel()
            {
                Title = item.Title,
                Des = item.Des,
                Text = item.Text,
            };
            return View(articleList);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ArticleListViewModel model)
        {
            IRegulatoryManager regulatoryManager = new RegulatoryManager();
            await regulatoryManager.EditRegulatory(model.Id, model.Title, model.Des, model.Text);
            BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            IRegulatoryManager regulatoryManager = new RegulatoryManager();
            DTO.RegulatoryInfoDto item = await regulatoryManager.GetOneRegulatory(id);
            ArticleListViewModel articleList = new ArticleListViewModel()
            {
                Id = item.Id,
                Title = item.Title,
                Des = item.Des,
                Text = item.Text,
                Writer = item.Name,
                CreateTime = item.CreateTime
            };
            return View(articleList);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(ArticleListViewModel model)
        {
            IRegulatoryManager regulatoryManager = new RegulatoryManager();
            await regulatoryManager.RemoveRegulatory(model.Id);
            BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
            return RedirectToAction("Index");
        }

    }
}