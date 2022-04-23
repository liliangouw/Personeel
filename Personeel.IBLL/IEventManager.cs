using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface IEventManager
    {
        //提交申请
        Task AddEvent(Guid userGuid, string eventSort, string eventReason,Guid departmentGuid);
        //修改申请
        Task EditEvent(Guid id, string eventSort, string eventReason);
        //获取个人所有申请
        Task<List<EventInfoDto>> GetAllEventByUserId(Guid userGuid);
        //获取单个申请的详细信息
        Task<EventInfoDto> GetOneById(Guid id);
        //撤销申请
        Task RemoveEvent(Guid id);

        //获取有所该部门，且状态为'部门主管申请'的审批
        Task<List<EventInfoDto>> GetAllEventByDep(Guid depGuid);
        //审批,根据权限不同进行不同阶段的审批
        Task EditEvent(Guid id,Guid userGuid, int power, bool pass,string eventSort, string leaveNotReason);
        //获取所有状态为‘人事审批的审批’
        Task<List<EventInfoDto>> GetAllEvent();
        //获取所有请假记录
        Task<List<EventInfoDto>> GetEventHistory();
    }
}
