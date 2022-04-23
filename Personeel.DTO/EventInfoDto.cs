using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class EventInfoDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        //员工名称
        public string UserName { get; set; }
        //事件类型
        public string EventSort { get; set; }
        //事件内容
        public string EventReason { get; set; }
        //审批状态
        public string EventState { get; set; }

        //备注
        public string EventNotReason { get; set; }
        //提交时间
        public DateTime ApproveTime { get; set; }

        //开始时间
        public DateTime StartTime { get; set; }

        //结束时间
        public DateTime EndTime { get; set; }

        //部门ID
        public Guid DepId { get; set; }
        //部门名称
        public string Department { get; set; }
    }

    public enum EventSort
    {
        调薪申请,
        转正申请,
        离职申请,
        调岗申请
    }
}
