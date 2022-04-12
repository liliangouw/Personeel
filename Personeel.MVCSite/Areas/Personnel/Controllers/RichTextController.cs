using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Personeel.BLL;
using Personeel.DTO;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class RichTextController : Controller
    {
        // GET: Personnel/RichText
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveInfo()
        {
            string textValue = Request.Form["textContent"];
            string textTitle = Request.Form["textTitle"];
            string textDes = Request.Form["textDes"];
            IBLL.IAnnounceManager announceManager = new AnnounceManager();
            announceManager.AddAnnounce(textTitle, textDes, textValue, Guid.Parse(Session["userId"].ToString()));
            return Content("save success");
        }
        //显示文章
        public async Task<ActionResult> ViewE(Guid id)
        {
            IBLL.IAnnounceManager announceManager = new AnnounceManager();
            AnnounceInfoDto info= await announceManager.GetOneAnnounce(id);
            ViewBag.File = info.Text.ToString();
            return View();
        }
    }
}