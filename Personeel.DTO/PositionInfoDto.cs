using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class PositionInfoDto
    {
        public  Guid PositionGuid { get; set; }
        public  string PositionName { get; set; }
        public  string PositionDescribe { get; set; }
    }
}
