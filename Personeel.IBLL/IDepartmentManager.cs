using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.IBLL
{
    public interface IDepartmentManager
    {
        Task AddDep(string depName, string depDes,Guid depUser);
        Task EditDep(string depName, string depDes,Guid depUser);
        Task RemoveDep(Guid id);
        Task<List<DTO.DepInfoDto>>GetInfo();
        Task<DTO.DepInfoDto> GetInfoById(Guid id);
        Task<DTO.DepInfoDto> GetInfoByName(string depName);
    }
}
