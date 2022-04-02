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
        public string DepName { get; set; }
        [Required]
        public string DepDes { get; set; }
    }
}