using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.IBLL
{
    public  interface IAnnounceManager
    {
        Task AddAnnounce(string title, string des, string text, Guid userId);
        Task<List<DTO.AnnounceInfoDto>> GetAllAnnounce();
        Task RemoveAnnounce(Guid id);
        Task EditAnnounce(Guid id, string title, string des, string text);

        Task<DTO.AnnounceInfoDto> GetOneAnnounce(Guid id);
    }
}
