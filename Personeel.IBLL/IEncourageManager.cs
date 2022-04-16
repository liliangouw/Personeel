using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface IEncourageManager
    {
        Task AddEncourage(Guid userGuid, string category, string reason, int money);
        Task<List<EncourageInfoDto>> GetAll();
        Task<EncourageInfoDto> GetOneById(Guid id);
        Task Edit(Guid id, string category, string reason, int money);
        Task Remove(Guid id);
    }
}
