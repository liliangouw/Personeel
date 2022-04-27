using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.AssessViewModels
{
    public class AddAssessViewModel
    {
        [Display(Name = "员工")]
        public List<Guid>UserGuid { get; set; }
        [Display(Name = "考核名称")]
        public string AssessName { get; set; }
        [Display(Name = "考核类别")]
        public string AssessType { get; set; }
    }
}