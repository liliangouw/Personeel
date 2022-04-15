using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    /// <summary>
    /// 请假
    /// </summary>
    public class AskForLeave:BaseEntity
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
        /// 请假类型
        /// </summary>
        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string LeaveSort { get; set; }
        /// <summary>
        /// 请假内容
        /// </summary>
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string LeaveReason { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string LeaveState { get; set; }
        /// <summary>
        /// 不通过原因
        /// </summary>
        [StringLength(100), Column(TypeName = "varchar")]
        public string LeaveNotReason { get; set; }
        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime ApproveTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [ForeignKey(nameof(Department))]
        public Guid DepId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public Department Department { get; set; }
    }
}
