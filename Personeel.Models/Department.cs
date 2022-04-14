using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class Department:BaseEntity
    {

        /// <summary>
        /// 名称
        /// </summary>
        [Required,StringLength(20), Column(TypeName = "varchar")]
        public string Depname { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(200), Column(TypeName = "varchar")]
        public string Depdescribe { get; set; }
        /// <summary>
        /// 部门主管
        /// </summary>
        public Guid UserGuid { get; set; }
    }
}
