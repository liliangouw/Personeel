using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.SalaryViewModels
{
    public class ChangeSalaryRecordViewModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }
        [Display(Name = "用户ID")]
        public Guid UserId { get; set; }
        [Display(Name = "用户名称")]
        public  string UserName { get; set; }
        [Display(Name = "调薪原因")]
        public  string Reason { get; set; }
        [Display(Name = "调前薪资")]
        public  int BeforeSalary { get; set; }
        [Display(Name = "调后薪资")]
        public  int ChangedSalary { get; set; }
        [Display(Name = "调薪时间")]
        public  DateTime ChangeDateTime { get; set; }
    }
}