using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.UserViewModels
{
    public class AddViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "账号")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 6)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [Compare(otherProperty: nameof(Password))]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [Required] [Display(Name = "姓名")] 
        public string Name { get; set; }
        [Required] [Display(Name = "权限")]
        public string Right { get; set; }

        [Required] [Display(Name = "基本工资")] 
        public int BasicMoney { get; set; }
        [Required] [Display(Name = "部门")] 
        public string Department { get; set; }

        [Required] [Display(Name = "职位")]
        public string Position { get; set; }


    }
}