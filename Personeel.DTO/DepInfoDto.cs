using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class DepInfoDto
    {
        public  Guid DepGuid { get; set; }
        public string DepName { get; set; }
       public string DepDes { get; set; }
       public string DepUser { get; set; }
       public  Guid DepUserGuid { get; set; }
    }
}
