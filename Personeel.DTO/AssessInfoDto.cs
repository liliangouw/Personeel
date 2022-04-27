using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
     public class AssessInfoDto
    {
        public Guid Id { get; set; }
        public string AssessName { get; set; }
        public string AssessType { get; set; }
        public string UserName { get; set; }
        public string UserDep { get; set; }
        public string Result { get; set; }
        public string State { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public enum State
    {
        待考核,
        已完成
    }
}
