using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface IAskForLeaveManager
    {
        //提交申请
        Task AddAskForLeave(Guid userGuid,string leaveSort,string leaveReason,DateTime startDate,DateTime endDate);
        //获取个人所有申请
        Task<List<AskLeaveInfoDto>> GetAllAskByUserId(Guid userGuid);
        //获取单个申请的详细信息
        Task<AskLeaveInfoDto> GetOneById(Guid id);
        //撤销申请
        Task RemoveAsk(Guid id);

        //获取有所该部门，且状态为'部门主管申请'的审批
        Task<List<AskLeaveInfoDto>> GetAllAskByDep(Guid depGuid);
        //审批,根据权限不同进行不同阶段的审批
        Task EditAsk(Guid id,Guid userRightGuid,bool pass,string leaveNotReason);
        //获取所有状态为‘人事审批的审批’
        Task<List<AskLeaveInfoDto>> GetAllAsk();
        //获取所有请假记录
        Task<List<AskLeaveInfoDto>> GetAskHistory();
        Task<List<AskLeaveInfoDto>> GetAskHistoryByDepId(Guid depGuid);
    }
}
