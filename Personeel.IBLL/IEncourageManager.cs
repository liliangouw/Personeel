using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.IBLL
{
    public interface IEncourageManager
    {
        Task AddEncourage(Guid userGuid, string category, string reason, int money);
        Task GetAll();
        Task GetOneById(Guid id);
        Task Edit(Guid userGuid, string category, string reason, int money);
        Task Remove(Guid id);
    }
}
