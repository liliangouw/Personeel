using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Models.TrainViewModels;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class TrainController : Controller
    {
        // GET: Personnel/Train
        public async  Task<ActionResult> Index()
        {
            ITrainManager trainManager = new TrainManager();
            List<TrainInfoDto>trainInfo= await trainManager.GetAllTrain();
            List<AddTrainViewModel> trainViewModel = new List<AddTrainViewModel>();
            foreach (var item in trainInfo)
            {
                AddTrainViewModel temp = new AddTrainViewModel()
                {
                    
                    TrainSort = item.TrainSort,
                    TrainDes = item.TrainDes,
                    TrainGuid = item.TrainGuid,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    UserId = item.UserId,
                    UserName = item.UserName
                };
                trainViewModel.Add(temp);
            }
            return View(trainViewModel);
        }

        // GET: Personnel/Train/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            ITrainManager trainManager = new TrainManager();
            TrainInfoDto info=await trainManager.GetOneById(id);
            AddTrainViewModel train = new AddTrainViewModel()
            {
                TrainSort = info.TrainSort,
                TrainDes = info.TrainDes,
                TrainGuid = info.TrainGuid,
                StartTime = info.StartTime,
                EndTime = info.EndTime,
                UserId = info.UserId,
                UserName = info.UserName
            };
            return View(train);
        }

        // GET: Personnel/Train/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            return View();
        }

        // POST: Personnel/Train/Create
        [HttpPost]
        public async Task<ActionResult> Create(AddTrainViewModel trainViewModel)
        {
            try
            {
                ITrainManager trainManager = new TrainManager();
                await trainManager.AddTrain(trainViewModel.TrainSort, trainViewModel.TrainDes, trainViewModel.StartTime,
                    trainViewModel.EndTime, trainViewModel.UserId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personnel/Train/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            ITrainManager trainManager = new TrainManager();
            TrainInfoDto info = await trainManager.GetOneById(id);
            AddTrainViewModel train = new AddTrainViewModel()
            {
                TrainSort = info.TrainSort,
                TrainDes = info.TrainDes,
                TrainGuid = info.TrainGuid,
                StartTime = info.StartTime,
                EndTime = info.EndTime,
                UserId = info.UserId,
                UserName = info.UserName
            };
            return View(train);
        }

        // POST: Personnel/Train/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, AddTrainViewModel trainView)
        {
            try
            {
                ITrainManager trainManager = new TrainManager();
                trainManager.EditTrain(id, trainView.TrainSort, trainView.TrainDes, trainView.StartTime,
                    trainView.EndTime, trainView.UserId);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personnel/Train/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            ITrainManager trainManager = new TrainManager();
            TrainInfoDto info = await trainManager.GetOneById(id);
            AddTrainViewModel train = new AddTrainViewModel()
            {
                TrainSort = info.TrainSort,
                TrainDes = info.TrainDes,
                TrainGuid = info.TrainGuid,
                StartTime = info.StartTime,
                EndTime = info.EndTime,
                UserId = info.UserId,
                UserName = info.UserName
            };
            return View(train);
        }

        // POST: Personnel/Train/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            try
            {
                ITrainManager trainManager = new TrainManager();
                await trainManager.RemoveTrain(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
