using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class AnnounceInfoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Des { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }
    }
}
