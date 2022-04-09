using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.ArticleViewModels
{
    public class ArticleListViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "描述")]
        public string Des { get; set; }
        [Display(Name = "内容")]
        public string Text { get; set; }
        [Display(Name = "发布人")]
        public string Writer { get; set; }
        [Display(Name = "创建时间")]
        public  DateTime CreateTime { get; set; }
    }
}