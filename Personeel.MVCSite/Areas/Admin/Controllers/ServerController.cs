using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Personeel.DTO;

namespace Personeel.MVCSite.Areas.Admin.Controllers
{
    public class ServerController : Controller
    {
        private PerformanceCounter performance = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
        // GET: Admin/Server
        public ActionResult Index()
        {
            
            string query = "Select * from {0}";
            ///获取操作系统数据
            SelectQuery queryOS = new SelectQuery(string.Format(query, WindowAPIType.Win32_OperatingSystem));
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(queryOS);
            foreach (ManagementBaseObject os in searcher.Get())
            {
                ViewBag.Version = Convert.ToString(os["Version"]);
                ViewBag.Caption = Convert.ToString(os["Caption"]);
                ViewBag.SerialNumber = Convert.ToString(os["SerialNumber"]);
                ViewBag.SystemDirectory = Convert.ToString(os["SystemDirectory"]);
                ViewBag.OSArchitecture = Convert.ToString(os["OSArchitecture"]);
                ViewBag.InstallDate = Convert.ToString(os["InstallDate"]);
                ViewBag.Organization = Convert.ToString(os["Organization"]);
            }
            TimeSpan ts = TimeSpan.FromMilliseconds(Environment.TickCount);
            ViewBag.time = ts.ToString();
            ViewBag.Cpu= this.performance.NextValue().ToString("F1") + "%";
            return View();
        }
    }
}