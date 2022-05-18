using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.ArticleViewModels;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class AnnounceController : Controller
    {
        // GET: Admin/Regulatory
        public async Task<ActionResult> Index(int id = 1, string Name = "")
        {
            IAnnounceManager regulatoryManager = new AnnounceManager();
            List<DTO.AnnounceInfoDto> regulatoryInfo = await regulatoryManager.GetAllAnnounce();
            if (Name == "")
            {

            }
            else
            {
                regulatoryInfo = regulatoryInfo.Where(m => m.Name.Contains(Name)).ToList();
            }
            List<ArticleListViewModel> articleList = new List<ArticleListViewModel>();
            foreach (var item in regulatoryInfo)
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
            var model = articleList.ToPagedList(id, 8);
            return View(model);
        }
        [HttpGet]
        public ActionResult CreateArticle()
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
                IAnnounceManager regulatoryManager = new AnnounceManager();
                regulatoryManager.AddAnnounce(model.Title, model.Des, model.Text, userid);
                BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }

        public async Task<ActionResult> Details(Guid id)
        {

            IAnnounceManager regulatoryManager = new AnnounceManager();
            DTO.AnnounceInfoDto item = await regulatoryManager.GetOneAnnounce(id);
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
            IAnnounceManager regulatoryManager = new AnnounceManager();
            DTO.AnnounceInfoDto item = await regulatoryManager.GetOneAnnounce(id);
            ArticleListViewModel articleList = new ArticleListViewModel()
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
            IAnnounceManager regulatoryManager = new AnnounceManager();
            await regulatoryManager.EditAnnounce(model.Id, model.Title, model.Des, model.Text);
            BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            IAnnounceManager regulatoryManager = new AnnounceManager();
            DTO.AnnounceInfoDto item = await regulatoryManager.GetOneAnnounce(id);
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
            IAnnounceManager regulatoryManager = new AnnounceManager();
            await regulatoryManager.RemoveAnnounce(model.Id);
            BaseManager.AddOperation(Guid.Parse(Session["userId"].ToString()), Request.RequestContext.RouteData.Values["controller"].ToString() + ":" + Request.RequestContext.RouteData.Values["action"].ToString());
            return RedirectToAction("Index");
        }
    }
}
