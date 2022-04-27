using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    //考核表
    public class Assess:BaseEntity
    {
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string AssessName { get; set; }
        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string AssessType { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserGuid { get; set; }
        public User User { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string Result { get; set; } = "-/-";

        public int State { get; set; } = 0;

    }
}
