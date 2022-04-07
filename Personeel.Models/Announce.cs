using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class Announce:BaseEntity
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
        /// 标题
        /// </summary>
        [Required, StringLength(50), Column(TypeName = "varchar")]
        public string AnnounceTitle { get; set; }
        /// <summary>
        /// 通知描述
        /// </summary>
        [Required, StringLength(50), Column(TypeName = "varchar")]
        public string AnnounceDes { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required, Column(TypeName = "ntext")]
        public string AnnounceText { get; set; }
    }
}
