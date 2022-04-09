using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.ArticleViewModels
{
    public class CreateArticleViewModel
    {
        [Required]
        [StringLength(maximumLength:50,MinimumLength = 2)]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [StringLength(maximumLength: 200, MinimumLength = 2)]
        [Display(Name = "描述")]
        public string Des { get; set; }
        public string Text { get; set; }
    }
}