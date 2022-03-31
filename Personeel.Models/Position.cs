using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class Position:BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required,StringLength(20), Column(TypeName = "varchar")]
        public string Posname { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(200), Column(TypeName = "varchar")]
        public string Posdescribe { get; set; }
    }
}
