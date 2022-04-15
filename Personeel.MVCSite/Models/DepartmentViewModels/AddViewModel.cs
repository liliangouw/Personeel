using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.DepartmentViewModels
{
    public class AddViewModel
    {
        [Required]
        [Display(Name = "部门名称")]
        public string DepName { get; set; }
        [Required]
        [Display(Name = "部门描述")]
        public string DepDes { get; set; }

        [Required]
        [Display(Name = "部门主管")]
        public Guid DepUser { get; set; }
    }
}