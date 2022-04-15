using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.IBLL
{
    public interface ITrainManager
    {
        Task AddTrain(string trainSort,string trainDes,DateTime startTime,DateTime endTime,Guid userGuid);
        Task<List<DTO.TrainInfoDto>> GetAllTrain();
        Task<DTO.TrainInfoDto> GetOneById(Guid id);
        Task EditTrain(Guid id, string trainResult, string trainComment);
        Task RemoveTrain(Guid id);
    }
}
