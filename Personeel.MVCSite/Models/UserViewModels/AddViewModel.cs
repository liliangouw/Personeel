using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.UserViwModels
{
    public class AddViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength:50,MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Compare(otherProperty:nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        public  string Name { get; set; }
        [Required]
        public  string Right { get; set; }
        [Required]
        public  int BasicMoney { get; set; }
    }
}