using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.EventViewModels
{
    public class EventIndexViewModel
    {
        [Display(Name = "id")]
        public Guid Id { get; set; }
        [Display(Name = "员工ID")]
        public Guid UserId { get; set; }
        [Display(Name = "员工名称")]
        public string UserName { get; set; }
        [Display(Name = "类型")]
        public string EventSort { get; set; }
        [Display(Name = "详情")]
        public string EventReason { get; set; }
        [Display(Name = "审批状态")]
        public string EventState { get; set; }

        [Display(Name = "备注")]
        public string EventNotReason { get; set; }
        [Display(Name = "提交时间")]
        public DateTime ApproveTime { get; set; }

        [Display(Name = "开始时间")]
        public DateTime StartTime { get; set; }

        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }

        [Display(Name = "部门ID")]
        public Guid DepId { get; set; }
        [Display(Name = "部门名称")]
        public string Department { get; set; }
    }
}