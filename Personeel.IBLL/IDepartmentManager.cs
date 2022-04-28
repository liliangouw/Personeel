using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface IDepartmentManager
    {
        //部门管理
        Task AddDep(string depName, string depDes,Guid depUser);
        Task EditDep(Guid depGuid, string depName, string depDes, Guid depUser);
        Task RemoveDep(Guid id);
        Task<List<DTO.DepInfoDto>>GetInfo();
        Task<DTO.DepInfoDto> GetInfoById(Guid id);
        Task<DTO.DepInfoDto> GetInfoByName(string depName);

        //部门岗位调动管理
        Task<List<ChangeDepartmentInfoDto>> GetAllUserDepartment();//获取所有人员的岗位信息
        Task<List<ChangeDepartmentInfoDto>> GetAllTransferRecords();//获取所有调岗记录
        Task<ChangeDepartmentInfoDto> GetOneUserDepartment(Guid userGuid);//获取个人的信息
        Task CreateTransferInfo(Guid userGuid, string transferDep, string transferPos,string reason);//添加调岗信息
        Task RemoveTransferInfo(Guid id);//删除调岗记录

    }
}
