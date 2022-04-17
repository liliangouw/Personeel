using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.CheckingViewModels
{
    public class CheckingViewModel
    {
        [Display(Name = "ID")]
        public  Guid Id { get; set; }
        [Display(Name = "用户ID")]
        public Guid UserGuid { get; set; }
        [Display(Name = "用户姓名")]
        public string UserName { get; set; }
        [Display(Name = "上班时间")]
        public DateTime BeginTime { get; set; } = DateTime.Now;
        [Display(Name = "下班时间")]
        public DateTime EndTime { get; set; }
        [Display(Name = "打卡状态")]
        public string Status { get; set; }
        [Display(Name = "日期")]
        public string DayTime { get; set; }
    }
}