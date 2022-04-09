using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Personeel.MVCSite.Controllers;
using Personeel.MVCSite.Filters;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    [PersonnelAuth]
    public class AdminController : MVCSite.Controllers.HomeController
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}