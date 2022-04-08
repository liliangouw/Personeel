using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personeel.MVCSite.Areas.PersonnelAdmin.Controllers
{
    public class PersonnelController : Controller
    {
        // GET: PersonnelAdmin/Personnel
        public ActionResult Index()
        {
            return View();
        }
    }
}