using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Personeel.BLL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.MVCSite.Models.FileViewModels;
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


        public ITrainInfoManager TrainInfoManager = new TrainInfoManager();
        // GET: Personnel/TrainInfo
        public async Task<ActionResult> Info()
        {
            List<TrainInfoInfoDto> info = await TrainInfoManager.GetAll();
            List<TrainInfoViewModel> list = info.Select(m => new TrainInfoViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Sort = m.Sort,
                FilePath = m.FilePath,
                CreateTime = m.CreateTime
            }).ToList();
            return View(list);
        }
        //下载文件
        public ActionResult Download(string filePath, string fileName)
        {
            Encoding encoding;
            string outputFileName = null;
            fileName = fileName.Replace("'", "");

            string browser = Request.UserAgent.ToUpper();
            if (browser.Contains("MS") == true && browser.Contains("IE") == true)
            {
                outputFileName = HttpUtility.UrlEncode(fileName);
                encoding = Encoding.Default;
            }
            else if (browser.Contains("FIREFOX") == true)
            {
                outputFileName = fileName;
                encoding = Encoding.GetEncoding("GB2312");
            }
            else
            {
                outputFileName = HttpUtility.UrlEncode(fileName);
                encoding = Encoding.Default;
            }
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.Charset = "UTF-8";
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = encoding;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + outputFileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }
    } 
}

