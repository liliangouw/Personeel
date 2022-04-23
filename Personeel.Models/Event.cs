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
    /// 事务
    /// </summary>
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
        /// 审批状态
        /// </summary>
        [Required]
        public int IsPassState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(100), Column(TypeName = "varchar")]
        public string NotReason { get; set; }
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
