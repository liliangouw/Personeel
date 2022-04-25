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
    /// 培训资料
    /// </summary>
    public class TrainInfo:BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required, StringLength(50), Column(TypeName = "varchar")]
        public string Title { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string Sort { get; set; }
        
        //文件路径
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string FilePath { get; set; }
    }
}
