using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.ArticleViewModels;

namespace Personeel.MVCSite.Areas.Employee.Controllers
{
    public class AnnounceController : Controller
    {
        // GET: Employee/Announce
        public async Task<ActionResult> Index()
        {
            IAnnounceManager regulatoryManager = new AnnounceManager();
            List<DTO.AnnounceInfoDto> regulatoryInfo = await regulatoryManager.GetAllAnnounce();
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
            return View(articleList);
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
    }
}