using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;
using Personeel.Models;

namespace Personeel.IBLL
{
    public interface ICheckingManager
    {
        Task Begin(Guid userGuid);
        Task End(Guid userid);
        Task<List<CheckingInfoDto>> GetAllChecking();
        Task<List<CheckingInfoDto>> GetCheckingByUserId(Guid userGuid);
        Task<List<CheckingInfoDto>> GetCheckingByDate(string day);
        Task RemoveChecking(Guid id);
    }
}
