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
        Task ChangeInfo(string posName, string posDescribe);
        Task RemovePosition(string posName);
        Task<DTO.PositionInfoDto> GetInfo();

    }
}
