﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Models.TrainViewModels;

namespace Personeel.MVCSite.Areas.Employee.Controllers
{
    public class TrainController : Controller
    {
        // GET: Employee/Train
        public async Task<ActionResult> Index()
        {
            ITrainManager trainManager = new TrainManager();
            List<TrainInfoDto> trainInfo = await trainManager.GetAllTrain();
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

        // GET: Employee/Train/Details/5
        public async Task<ActionResult> Details(Guid id)
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
    }

}
