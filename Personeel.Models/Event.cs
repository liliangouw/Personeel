using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class Event:BaseEntity
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
        /// 事务类型
        /// </summary>
        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string EventSort { get; set; }
        /// <summary>
        /// 事务内容
        /// </summary>
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string EventReason { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass { get; set; }
        /// <summary>
        /// 不通过原因
        /// </summary>
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string NotReason { get; set; }
        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime ApproveTime { get; set; }
    }
}
