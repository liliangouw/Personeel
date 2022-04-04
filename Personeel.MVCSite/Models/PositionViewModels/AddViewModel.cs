﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.PositionViewModels
{
    public class AddViewModel
    {
        [Display(Name = "职位名称")]
        public string PosName { get; set; }
        [Display(Name = "职位描述")]
        public string PosDescribe { get; set; }
    }
}