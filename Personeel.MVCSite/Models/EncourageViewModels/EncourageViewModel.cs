using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.EncourageViewModels
{
    public class EncourageViewModel
    {
        [Display(Name = "用户姓名")]
        public string UserName { get; set; }
        [Display(Name = "用户ID")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Display(Name = "分类")]
        [Required]
        public string Category { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        [Display(Name = "原因")]
        [Required]
        public string Reason { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
         [Display(Name = "金额")]
        [Required]
        public int Money { get; set; }

        public  Guid Id { get; set; }
    }
}