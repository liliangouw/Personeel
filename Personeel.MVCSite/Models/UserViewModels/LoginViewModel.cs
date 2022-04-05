using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "邮箱账号")]
        public string LoginName { get; set; }
        [Display(Name = "密码")]
        [Required]
        [StringLength(maximumLength:50,MinimumLength = 6)]
        [DataType(dataType:DataType.Password)]
        public  string LoginPwd { get; set; }
        [Display(Name = "记住我")]
        public  bool RememberMe { get; set; }
    }
}