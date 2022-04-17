using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class CheckingInfoDto
    {
        public  Guid Id { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string DayTime { get; set; }
    }
}
