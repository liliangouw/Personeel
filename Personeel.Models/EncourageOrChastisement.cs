using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class EncourageOrChastisement:BaseEntity
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
        /// 类别
        /// </summary>
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string Category { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Reason { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int Money { get; set; }
    }
}
