using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class ContractInfoDto
    {
        public Guid Id { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string DepName { get; set; }
        public int Years { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime DeadLine { get; set; }
        //文件路径
        public string FilePath { get; set; }
    }
}
