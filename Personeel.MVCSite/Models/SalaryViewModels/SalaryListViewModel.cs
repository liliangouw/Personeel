using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.SalaryViewModels
{
    public class SalaryListViewModel
    {

        public Guid Id { get; set; }

        [Display(Name = "用户名称")]
        public string UserName { get; set; }

        [Display(Name = "基本工资")]
        public int BasicSalary { get; set; }

        [Display(Name = "实发工资")]
        public int ActualSalary { get; set; }
        [Display(Name = "工资日期")]
        public string SalaryDate { get; set; }
    }
}