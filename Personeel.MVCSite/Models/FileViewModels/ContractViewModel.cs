using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.FileViewModels
{
    public class ContractViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Display(Name = "用户ID")]
        public Guid UserGuid { get; set; }
        [Display(Name = "员工名称")]
        public string UserName { get; set; }
        [Display(Name = "部门名称")]
        public string DepName { get; set; }
        [Display(Name = "年限")]
        public int Years { get; set; }
        [Display(Name = "开始时间")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "截至时间")]
        public DateTime DeadLine { get; set; }
        [Display(Name = "合同")]
        //文件路径
        public string FilePath { get; set; }
    }
}