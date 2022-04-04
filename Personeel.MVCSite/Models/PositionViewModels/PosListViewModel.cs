using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.PositionViewModels
{
    public class PosListViewModel
    {
        public Guid PosGuid { get; set; }
        [Required]
        [Display(Name = "职位名称")]
        public string PosName { get; set; }
        [Required]
        [Display(Name = "职位描述")]
        public string PosDes { get; set; }
    }
}