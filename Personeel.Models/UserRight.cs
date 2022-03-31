using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    public class UserRight:BaseEntity
    {
        public int UserPower { get; set; }
    }
}
