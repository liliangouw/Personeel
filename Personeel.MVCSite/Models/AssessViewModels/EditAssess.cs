using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.AssessViewModels
{
    public class EditAssess
    {
        public Guid Id { get; set; }
        [Display(Name = "考核名称")]
        public string AssessName { get; set; }
        [Display(Name = "考核类型")]
        public string AssessType { get; set; }
        [Display(Name = "部门")]
        public string AssessDep { get; set; }
        [Display(Name = "名称")]
        public string UserName { get; set; }
        [Display(Name = "结果")]
        public int Result { get; set; }
    }

}