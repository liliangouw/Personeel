using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.UserViewModels
{
    public class UserListViewModel
    {
        [Display(Name = "工号")]
        public  int UserNum { get; set; }
        [Display(Name = "姓名")]
        public  string Name { get; set; }
        [Display(Name = "部门")]
        public  string Department { get; set; }
        [Display(Name = "职位")]
        public  string Position { get; set; }
        [Display(Name = "邮箱")]
        public  string Email { get; set; }
        [Display(Name = "角色")]
        public  int UserPower { get; set; }
    }
}