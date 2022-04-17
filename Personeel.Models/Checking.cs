using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class Checking:BaseEntity
    {
        /// <summary>
        /// 用户外键
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserGuid { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 上班时间
        /// </summary>
        public DateTime BeginTime { get; set; }=DateTime.Now;
        /// <summary>
        /// 下班时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 打卡状态
        /// </summary>

        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string Status { get; set; }

        /// <summary>
        /// 当前年月日
        /// 2008-09-04
        /// </summary>
        [Required, StringLength(30), Column(TypeName = "varchar")]
        public string DayTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    }
}
