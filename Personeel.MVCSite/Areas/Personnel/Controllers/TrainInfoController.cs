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
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    
    public class TrainInfoController : Controller
    {
        public ITrainInfoManager TrainInfoManager = new TrainInfoManager();
        // GET: Personnel/TrainInfo
        public async Task<ActionResult> Index(int id = 1, string Name = "")
        {
            List<TrainInfoInfoDto> info = await TrainInfoManager.GetAll();
            if (Name == "")
            {

            }
            else 
            {
                info=info.Where(m => m.Title.Contains(Name)).ToList();

            }
            List<TrainInfoViewModel> list = info.Select(m => new TrainInfoViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Sort = m.Sort,
                FilePath = m.FilePath,
                CreateTime = m.CreateTime
            }).ToList();
            var model = list.ToPagedList(id, 8);
            return View(model);
        }


        // GET: Personnel/TrainInfo/Create
        public  ActionResult Create()
        {
            return View();
        }

        // POST: Personnel/TrainInfo/Create
        [HttpPost]
        public async Task<ActionResult> Create(TrainInfoViewModel model)
        {
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return Content("未选择文件");
            }
            var file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                //文件大小大（以字节为单位）为0时，做一些操作
                return Content("文件不能为空");
            }
            else
            {
                //文件大小不为0
                file = Request.Files[0];
                //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                //服务器上的UpLoadFile文件夹必须有读写权限
                string target = Server.MapPath("/") + ("/File/TrainInfo/");//取得目标文件夹的路径
                string filename = file.FileName;//取得文件名字
                string path = target + filename;//获取存储的目标地址
                file.SaveAs(path);
                await TrainInfoManager.Add(model.Title,model.Sort, path);
            }

            return RedirectToAction("Index");
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
        // GET: Personnel/TrainInfo/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            TrainInfoInfoDto info =await TrainInfoManager.GetOneById(id);
            TrainInfoViewModel list = new TrainInfoViewModel()
            {
                Id = info.Id,
                Title = info.Title,
                Sort = info.Sort,
                CreateTime = info.CreateTime
            };
            return View(list);
        }

        // POST: Personnel/TrainInfo/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, TrainInfoViewModel model)
        {
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return Content("未选择文件");
            }

            var file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                //文件大小大（以字节为单位）为0时，做一些操作
                return Content("文件不能为空");
            }
            else
            {
                //文件大小不为0
                file = Request.Files[0];
                //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                //服务器上的UpLoadFile文件夹必须有读写权限
                string target = Server.MapPath("/") + ("/File/TrainInfo/"); //取得目标文件夹的路径
                string filename = file.FileName; //取得文件名字
                string path = target + filename; //获取存储的目标地址
                file.SaveAs(path);
                await TrainInfoManager.Edit(id, model.Title, model.Sort, path);

                return RedirectToAction("Index");
            }
        }

        // GET: Personnel/TrainInfo/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            TrainInfoInfoDto info = await TrainInfoManager.GetOneById(id);
            TrainInfoViewModel list = new TrainInfoViewModel()
            {
                Id = info.Id,
                Title = info.Title,
                Sort = info.Sort,
                CreateTime = info.CreateTime
            };
            return View(list);
        }

        // POST: Personnel/TrainInfo/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
           await TrainInfoManager.Remove(id);
            return RedirectToAction("Index");
            
        }
    }
}
