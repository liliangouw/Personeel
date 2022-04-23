using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class AskLeaveInfoDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        //员工名称
        public string UserName { get; set; }
        //请假类型
        public string LeaveSort { get; set; }
        //请假内容
        public string LeaveReason { get; set; }
        //审批状态
        public string LeaveState { get; set; }

        //备注
        public string LeaveNotReason { get; set; }
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
    public enum AskState
    {
        部门主管审批,
        人事审批,
        审批通过,
        审批不通过,
        已撤销
    }

    public enum AskSort
    {
        年假,
        事假,
        病假,
        调休,
        婚嫁,
        产假,
        陪产假,
        丧假,
        其他
    }
}
