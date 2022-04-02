using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.PositionViewModels
{
    public class AddPosition
    {
        [Required]
        public string PosName { get; set; }
        [Required]
        public string PosDesc { get; set; }
    }
}