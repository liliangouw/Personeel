using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class Contract:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserGuid { get; set; }
        public User User { get; set; }
        public int Years { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime StartTime { get; set; }
        //文件路径
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string FilePath { get; set; }
    }
}
