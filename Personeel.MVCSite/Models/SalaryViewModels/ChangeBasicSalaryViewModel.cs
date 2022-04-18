using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.SalaryViewModels
{
    public class ChangeBasicSalaryViewModel
    {
        [Display(Name = "用户ID")]
        public  Guid UserGuid { get; set; }
        [Display(Name = "用户名称")]
        public string UserName { get; set; }
        [Display(Name = "基本工资")]
        public  int BasicSalary { get; set; }
        [Display(Name = "部门")]
        public  string Department { get; set; }
        [Display(Name = "职位")]
        public  string Position { get; set; }
        [Display(Name = "调薪原因")]
        public  string Reason { get; set; }
    }
}