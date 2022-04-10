using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.OperationViewModels
{
    public class OperationListViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "操作人")]
        public string UserName { get; set; }
        [Display(Name = "操作内容")]
        public string Text { get; set; }
    }
}