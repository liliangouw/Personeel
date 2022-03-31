using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class Train:BaseEntity
    {
        /// <summary>
        /// 用户外键
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 培训类别
        /// </summary>
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string TrainSort { get; set; }
        /// <summary>
        /// 培训内容
        /// </summary>
        [Required, StringLength(50), Column(TypeName = "varchar")]
        public string TrainDes { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 培训结果
        /// </summary>
        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string TrainResult { get; set; }
        /// <summary>
        /// 培训评价
        /// </summary>
        [Required, StringLength(200), Column(TypeName = "varchar")]
        public string TarinComment { get; set; }
    }
}
