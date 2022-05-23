using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using Personeel.BLL;
using Personeel.IBLL;
using Personeel.MVCSite.Models.SalaryViewModels;
using Webdiyer.WebControls.Mvc;

namespace Personeel.MVCSite.Areas.Personnel.Controllers
{
    public class SalaryController : Controller
    {
        ISalaryManager salaryManager = new SalaryManager();
        // GET: Personnel/Salary
        public async Task<ActionResult> Index(int id = 1, string Name = "", DateTime? start = null, DateTime? end = null)
        {
            start = start ?? new DateTime(2000, 1, 1, 0, 0, 0);
            end = end ?? DateTime.Now;
            var info=  await salaryManager.GetAllSalary();
            info = info.Where(m => m.Date >= start && m.Date <= end).ToList();
            if (Name == "")
            {

            }
            else 
            {
                info=info.Where(m => m.UserName.Contains(Name)).ToList();
            }
            var list = info.Select(m => new SalaryListViewModel()
          {
              Id = m.Id,
              UserName = m.UserName,
              BasicSalary = m.BasicSalary,
              ActualSalary = m.ActualSalary,
              SalaryDate = m.SalaryDate
             
          }).ToList();
            var model = list.ToPagedList(id, 8);
          return View(model);
        }

        public async Task<ActionResult> Excel(string Name = "", DateTime? start = null, DateTime? end = null)
        {
            start = start ?? new DateTime(2000, 1, 1, 0, 0, 0);
            end = end ?? DateTime.Now;
            var info = await salaryManager.GetAllSalary();
            info = info.Where(m => m.Date >= start && m.Date <= end).ToList();
            if (Name == "")
            {

            }
            else
            {
                info = info.Where(m => m.UserName.Contains(Name)).ToList();
            }
            //根据日期查询数据

            HSSFWorkbook excelBook = new HSSFWorkbook();//创建工作簿Excel
            NPOI.SS.UserModel.ISheet sheet = excelBook.CreateSheet("工资表单");//为工作簿创建工作表并命名
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);//创建第一行
            row.CreateCell(0).SetCellValue("员工");//创建其他列并赋值
            row.CreateCell(1).SetCellValue("基础工资");
            row.CreateCell(2).SetCellValue("奖惩");
            row.CreateCell(3).SetCellValue("应到天数");
            row.CreateCell(4).SetCellValue("实到天数");
            row.CreateCell(5).SetCellValue("补助");
            row.CreateCell(6).SetCellValue("公积金");
            row.CreateCell(7).SetCellValue("社保");
            row.CreateCell(8).SetCellValue("税额");
            row.CreateCell(9).SetCellValue("实发薪资");
            row.CreateCell(10).SetCellValue("发薪日期");
            
            for (int i = 0; i < info.Count(); i++)
            {
                NPOI.SS.UserModel.IRow rowTemp = sheet.CreateRow(i + 1);//创建行(根据具体数据写代码)
                rowTemp.CreateCell(0).SetCellValue(info[i].UserName.ToString());
                rowTemp.CreateCell(1).SetCellValue(info[i].BasicSalary.ToString());
                rowTemp.CreateCell(2).SetCellValue(info[i].EncourageOrChastisement.ToString());
                rowTemp.CreateCell(3).SetCellValue(info[i].ShouldDays.ToString());
                rowTemp.CreateCell(4).SetCellValue(info[i].ActualDays.ToString());
                rowTemp.CreateCell(5).SetCellValue(info[i].Subsidies.ToString());
                rowTemp.CreateCell(6).SetCellValue(info[i].Accumulationfund.ToString());
                rowTemp.CreateCell(7).SetCellValue(info[i].SocialSecurity.ToString());
                rowTemp.CreateCell(8).SetCellValue(info[i].Tax.ToString());
                rowTemp.CreateCell(9).SetCellValue(info[i].ActualSalary.ToString());
                rowTemp.CreateCell(10).SetCellValue(info[i].SalaryDate.ToString());

            }
            var fileName = "工资表单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";//文件名
            //将Excel表格转化为流，输出
            MemoryStream bookStream = new MemoryStream();//创建文件流
            excelBook.Write(bookStream);//文件写入流（向流中写入字节序列）
            bookStream.Seek(0, SeekOrigin.Begin);//输出之前调用Seek，把0位置指定为开始位置
            return File(bookStream, "application/vnd.ms-excel", fileName);//最后以文件形式返回
        }


        // GET: Personnel/Salary/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var info =  await salaryManager.GetInfoById(id);
            var list = new AddSalaryViewModel()
            {
                Id = info.Id,
                UserName = info.UserName,
                BasicSalary = info.BasicSalary,
                EncourageOrChastisement = info.EncourageOrChastisement,
                ShouldDays = info.ShouldDays,
                ActualDays = info.ActualDays,
                Accumulationfund = info.Accumulationfund,
                Subsidies = info.Subsidies,
                SocialSecurity = info.SocialSecurity,
                Tax = info.Tax,
                ActualSalary = info.ActualSalary,
                SalaryDate = info.SalaryDate
            };
            return View(list);
        }

        // GET: Personnel/Salary/Create
        public async Task<ActionResult> Select()
        {
            ViewBag.Users = await new UserManager().GetAllUser();
            return View();
        }

        // POST: Personnel/Salary/Create
        [HttpPost]
        public async Task<ActionResult> Select(SelectUserViewModel model)
        {
            try { 
            await salaryManager.AddSalarys(model.UserGuid);
            return  RedirectToAction("Index");}
            catch {
                ViewBag.Users = await new UserManager().GetAllUser();
                return View();
            }
        }


        // GET: Personnel/Salary/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var info = await salaryManager.GetInfoById(id);
            AddSalaryViewModel list = new AddSalaryViewModel()
            {
                UserName = info.UserName,
                UserId = info.UserId,
                BasicSalary = info.BasicSalary,
                ActualDays = info.ActualDays,
                ShouldDays = info.ShouldDays,
                EncourageOrChastisement = info.EncourageOrChastisement,
                SalaryDate = info.SalaryDate,
                ActualSalary = info.ActualSalary,
                Accumulationfund = info.Accumulationfund,
                Id = id,
                Tax = info.Tax,
                SocialSecurity = info.SocialSecurity,
                Subsidies = info.Subsidies,
            };
            return View(list);
        }

        // POST: Personnel/Salary/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, AddSalaryViewModel model)
        {
            try
            {
               await  salaryManager.EditSalary(id, model.BasicSalary, model.EncourageOrChastisement, model.ShouldDays,
                    model.ActualDays, model.Subsidies, model.Accumulationfund, model.SocialSecurity, model.Tax,
                    model.SalaryDate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personnel/Salary/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var info = await salaryManager.GetInfoById(id);
            AddSalaryViewModel list = new AddSalaryViewModel()
            {
                UserName = info.UserName,
                UserId = id,
                BasicSalary = info.BasicSalary,
                ActualDays = info.ActualDays,
                ShouldDays = info.ShouldDays,
                EncourageOrChastisement = info.EncourageOrChastisement,
                SalaryDate = info.SalaryDate,
                ActualSalary = info.ActualSalary,
                Accumulationfund = info.Accumulationfund,
                Id = info.Id,
                Tax = info.Tax,
                SocialSecurity = info.SocialSecurity,
                Subsidies = info.Subsidies,
            };
            return View(list);
        }

        // POST: Personnel/Salary/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                salaryManager.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
