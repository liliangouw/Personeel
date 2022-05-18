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
    public class ContractController : Controller
    {
        private IContractManager contractManager = new ContractManager();
        // GET: Personnel/Contract
        public async Task<ActionResult> Index(int id = 1, string Name = "")
        {
           List<ContractInfoDto>userList=await contractManager.GetAll();
            if (Name == "")
            {

            }
            else
            {
                userList = userList.Where(m => m.UserName.Contains(Name)).ToList();
            }
            List<ContractViewModel> list = userList.Select(m => new ContractViewModel()
           {
               Id = m.Id,
               UserName = m.UserName,
               DepName = m.DepName,
               CreateTime = m.CreateTime,
               Years = m.Years,
               DeadLine = m.DeadLine,
               FilePath = m.FilePath
           }).ToList();
            var model = list.ToPagedList(id, 8);
            return View(model);
        }

        // GET: Personnel/Contract/Create
        public async Task<ActionResult> Create()
        {
            ContractViewModel list = new ContractViewModel();
            list.CreateTime=DateTime.Now;
            ViewBag.Users = await new UserManager().GetAllUser();
            return View(list);
        }

        // POST: Personnel/Contract/Create
        [HttpPost]
        public async Task<ActionResult> Create(ContractViewModel model)
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
                    string target = Server.MapPath("/") + ("/File/Contract/");//取得目标文件夹的路径
                    string filename = file.FileName;//取得文件名字
                    string path = target + filename;//获取存储的目标地址
                    file.SaveAs(path);
                    await contractManager.Add(model.UserGuid, model.CreateTime, model.Years, path);
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

        // GET: Personnel/Contract/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ContractInfoDto info = await contractManager.GetOneById(id);
            ContractViewModel list = new ContractViewModel()
            {
                Id = info.Id,
                CreateTime = info.CreateTime,
                Years = info.Years,
            };
            return View(list);
        }

        // POST: Personnel/Contract/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, ContractViewModel model)
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
                string target = Server.MapPath("/") + ("/File/Contract/"); //取得目标文件夹的路径
                string filename = file.FileName; //取得文件名字
                string path = target + filename; //获取存储的目标地址
                file.SaveAs(path);
                await contractManager.Edit(id, model.CreateTime, model.Years,path);

                return RedirectToAction("Index");

            }
        }

        // GET: Personnel/Contract/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            ContractInfoDto info = await contractManager.GetOneById(id);
            ContractViewModel list = new ContractViewModel()
            {
                Id = info.Id,
                UserName = info.UserName,
                CreateTime = info.CreateTime,
                Years = info.Years,
                DeadLine = info.DeadLine,
                DepName = info.DepName
            };
            return View(list);
        }

        // POST: Personnel/Contract/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            
                await contractManager.Remove(id);

                return RedirectToAction("Index");
            
        }
    }
}
