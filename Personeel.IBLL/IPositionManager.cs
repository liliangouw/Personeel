using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.IBLL
{
    public interface IPositionManager
    {
        Task AddPosition(string posName, string posDescribe);
        Task ChangeInfo(Guid id ,string posName, string posDescribe);
        Task RemovePosition(Guid Id);
        Task<List<DTO.PositionInfoDto>> GetInfo();
        Task<DTO.PositionInfoDto> GetInfoById(Guid id);

    }
}
