using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.SalaryViewModels
{
    public class AddSalaryViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [Display(Name = "用户名称")]
        public string UserName { get; set; }

        [Display(Name = "基本工资")]
        public int BasicSalary { get; set; }

        [Display(Name = "奖惩")]
        public int EncourageOrChastisement { get; set; }
        [Display(Name = "应到天数")]
        public int ShouldDays { get; set; }
        [Display(Name = "实到天数")]
        public int ActualDays { get; set; }
        [Display(Name = "补助")]
        public int Subsidies { get; set; }
        [Display(Name = "公积金")]
        public int Accumulationfund { get; set; }
        [Display(Name = "社保")]
        public int SocialSecurity { get; set; }
        [Display(Name = "税额")]
        public int Tax { get; set; }
        [Display(Name = "实发工资")]
        public int ActualSalary { get; set; }
        [Display(Name = "工资日期")]
        public string SalaryDate { get; set; }
    }
}