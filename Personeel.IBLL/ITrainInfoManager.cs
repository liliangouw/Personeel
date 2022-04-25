using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface ITrainInfoManager
    {
        Task<List<TrainInfoInfoDto>> GetAll();
        Task Add(string title, string sort, string filePath);
        Task<TrainInfoInfoDto> GetOneById(Guid id);
        Task Edit(Guid id, string title,string sort, string filePath);
        Task Remove(Guid id);
    }
}
