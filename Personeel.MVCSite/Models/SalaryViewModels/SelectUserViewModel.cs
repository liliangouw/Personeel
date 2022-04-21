using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.SalaryViewModels
{
    public class SelectUserViewModel
    {
        [Display(Name = "用户名称")]
        public List<Guid> UserGuid { get; set; }
    }
}