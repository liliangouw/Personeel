using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Personeel.MVCSite.Models.DepartmentViewModels
{
    public class DepListViewModel
    {
        public Guid DepGuid { get; set; }
        [Required]
        [Display(Name = "部门名称")]
        public string DepName { get; set; }
        [Required]
        [Display(Name = "部门描述")]
        public string DepDes { get; set; }
        [Required]
        [Display(Name = "部门主管ID")]
        public Guid DepUserId { get; set; }

        [Required]
        [Display(Name = "部门主管")]
        public string  DepUser { get; set; }
    }
}