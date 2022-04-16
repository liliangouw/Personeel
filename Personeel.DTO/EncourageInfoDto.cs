using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class EncourageInfoDto
    {
        public  Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        
        public string Category { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        
        public string Reason { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int Money { get; set; }
    }
}
