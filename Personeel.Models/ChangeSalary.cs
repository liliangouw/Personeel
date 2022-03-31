using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class ChangeSalary:BaseEntity
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
        /// 调薪原因
        /// </summary>
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string ChangeReason { get; set; }
        /// <summary>
        /// 调后薪资
        /// </summary>
        [Required]
        public int ChangedSalary { get; set; }
    }
}
