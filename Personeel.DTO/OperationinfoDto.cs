using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class OperationinfoDto
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public  string Name { get; set; }
        public  string Text { get; set; }
    }
}
