using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class ChangeDepartmentInfoDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string TransferReason { get; set; }
        public string BeforeDep { get; set; }
        public string BeforePos { get; set; }
        public string TransferDep { get; set; }
        public string TransferPos { get; set; }
        public DateTime TransferDateTime { get; set; }
    }
}
