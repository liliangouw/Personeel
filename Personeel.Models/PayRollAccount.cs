using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class PayRollAccount:BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserGuid { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 基础薪资
        /// </summary>
        public int BasicSalary { get; set; }
        /// <summary>
        /// 补助
        /// </summary>
        public int Subsidies { get; set; }
        /// <summary>
        /// 公积金
        /// </summary>
        public int Accumulationfund { get; set; }
        /// <summary>
        /// 社保
        /// </summary>
        public int SocialSecurity { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        public int Tax { get; set; }
    }
}
