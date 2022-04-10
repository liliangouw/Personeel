using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.IBLL
{
    public interface IRegulatoryManager
    {
        Task AddRegulatory(string title, string des, string text,Guid userId);
        Task<List<DTO.RegulatoryInfoDto>> GetAllRegulatory();
        Task RemoveRegulatory(Guid id);
        Task EditRegulatory(Guid id, string title, string des,string text);

        Task<DTO.RegulatoryInfoDto> GetOneRegulatory(Guid id);
    }
}
