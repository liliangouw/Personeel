using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class TrainInfoDto
    {
        public  Guid TrainGuid { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        
        public string TrainSort { get; set; }
        /// <summary>
        /// 培训内容
        /// </summary>
        public string TrainDes { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 培训结果
        /// </summary>
        public string TrainResult { get; set; }

        /// <summary>
        /// 培训评价
        /// </summary>
        public string TrainComment { get; set; }
    }
}
