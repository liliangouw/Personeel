using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.DepartmentViewModels
{
    public class UserDepartmentsViewModel
    {
        public Guid UserGuid { get; set; }
        [Display(Name = "用户名称")]
        public  string UserName { get; set; }
        [Display(Name = "用户部门")]
        public string UserDep { get; set; }
        [Display(Name = "用户岗位")]
        public string UserPos { get; set; }
    }
}