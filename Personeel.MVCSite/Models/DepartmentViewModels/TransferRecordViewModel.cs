using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.DepartmentViewModels
{
    public class TransferRecordViewModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }
        [Display(Name = "用户Id")]
        public Guid UserGuid { get; set; }
        [Display(Name = "用户名称")]
        public string UserName { get; set; }
        [Display(Name = "调前部门")]
        public string BeforeDep { get; set; }
        [Display(Name = "调前职位")]
        public string BeforePos { get; set; }
        [Display(Name = "调后部门")]
        public string TransferDep { get; set; }
        [Display(Name = "调后职位")]
        public string TransferPos { get; set; }
        [Display(Name = "调动原因")]
        public string Reason { get; set; }
        [Display(Name = "调动时间")]
        public DateTime TransferDateTime { get; set; }

    }
}