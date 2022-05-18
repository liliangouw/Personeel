using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Models.CheckingViewModels;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class CheckingController : Controller
    {
        public ICheckingManager checkingManager = new CheckingManager();
        // GET: Personnel/Checking
        public async Task<ActionResult> Index(DateTime? start = null, DateTime? end = null, string Name = "",int id = 1)
        {
            start = start ?? new DateTime(2000, 1, 1, 0, 0, 0);
            end = end ?? DateTime.Now;
            List<CheckingInfoDto> info = await checkingManager.GetAllChecking();
            info = info.Where(m => m.BeginTime >= start && m.BeginTime <= end).ToList();
            if (Name == "")
            {

            }
            else
            {
                info=info.Where(m => m.UserName.Contains(Name)).ToList();
            }
            List<CheckingViewModel> list = new List<CheckingViewModel>();
            foreach (var item in info)
            {
                CheckingViewModel temp = new CheckingViewModel()
                {
                    Id = item.Id,
                    BeginTime = item.BeginTime,
                    DayTime = item.DayTime,
                    EndTime = item.EndTime,
                    UserName = item.UserName,
                    Status = item.Status
                };
                list.Add(temp);
            }
            var model = list.ToPagedList(id, 8);
            return View(model);
        }
    }
}