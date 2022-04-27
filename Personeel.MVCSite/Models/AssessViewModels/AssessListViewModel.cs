using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.AssessViewModels
{
    public class AssessListViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "考核名称")]
        public string AssessName { get; set; }
        [Display(Name = "类别")]
        public string AssessType { get; set; }
        [Display(Name = "员工")]
        public string UserName { get; set; }
        [Display(Name = "部门")]
        public string UserDep { get; set; }
        [Display(Name = "结果")]
        public string Result { get; set; }
        [Display(Name = "状态")]
        public string State { get; set; }
        [Display(Name = "考核时间")]
        public DateTime CreateTime { get; set; }
    }
}