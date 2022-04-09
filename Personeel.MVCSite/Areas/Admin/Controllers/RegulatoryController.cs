using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.MVCSite.Filters;
using Personeel.MVCSite.Models.ArticleViewModels;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    [PersonnelAuth]
    public class RegulatoryController : Controller
    {
        // GET: Admin/Regulatory
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> CreateArticle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(CreateArticleViewModel model)
        {
            return View();
        }
    }
}