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

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class CheckingController : Controller
    {
        public ICheckingManager checkingManager = new CheckingManager();
        // GET: Personnel/Checking
        public async Task<ActionResult> Index()
        {
            
            List<CheckingInfoDto> info = await checkingManager.GetAllChecking();
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
            return View(list);
        }
    }
}