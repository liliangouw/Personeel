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
    /// 人事调动
    /// </summary>
    public class Transfer:BaseEntity
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
        /// 调动原因
        /// </summary>
        [Required, StringLength(50), Column(TypeName = "varchar")]
        public string TransferReason { get; set; }
        /// <summary>
        /// 调前部门
        /// </summary>
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string BeforeTransferDep { get; set; }
        /// <summary>
        /// 调前岗位
        /// </summary>
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string BeforeTransferPos { get; set; }

        /// <summary>
        /// 调后部门
        /// </summary>
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string TransferDep { get; set; }
        /// <summary>
        /// 调后岗位
        /// </summary>
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string TransferPos { get; set; }
    }
}
