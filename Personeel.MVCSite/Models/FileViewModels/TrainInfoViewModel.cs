using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.FileViewModels
{
    public class TrainInfoViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "名称")]
        public string Title { get; set; }
        [Display(Name = "描述")]
        public string Sort { get; set; }
        [Display(Name = "文件")]
        public string FilePath { get; set; }
        [Display(Name = "上传时间")]
        public DateTime CreateTime { get; set; }
    }
}