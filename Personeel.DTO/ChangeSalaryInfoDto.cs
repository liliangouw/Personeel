using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class ChangeSalaryInfoDto
    {
        public Guid Id { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string ChangeReason { get; set; }
        public int BeforeSalary { get; set; }
        public int ChangedSalary { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
