using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.TrainViewModels
{
    public class AddTrainViewModel
    {
        [Display(Name = "培训ID")]
        public Guid TrainGuid { get; set; }
        [Display(Name = "主讲人")]
        public Guid UserId { get; set; }
        [Display(Name = "主讲人名称")]
        public string UserName { get; set; }
        [Display(Name = "培训分类")]

        public string TrainSort { get; set; }
        /// <summary>
        /// 培训内容
        /// </summary>
         [Display(Name = "培训内容")]
        public string TrainDes { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }
    }
}