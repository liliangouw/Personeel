using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface IAssessManager
    {
        Task Add(string name,string type,List<Guid>userGuid);
        Task<List<AssessInfoDto>> GetAll();
        Task<AssessInfoDto> GetOneById(Guid id);
        Task Edit(Guid id, int grade, int state);
        Task Remove(Guid id);
        Task<List<AssessInfoDto>> GetAllByUserId(Guid userGuid);
    }
}
