using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface IContractManager
    {
        Task<List<ContractInfoDto>> GetAll();
        Task Add(Guid userGuid,DateTime startTime,int year,string filePath);
        Task<ContractInfoDto> GetOneById(Guid id);
        Task Edit(Guid id,DateTime startTime, int year, string filePath);
        Task Remove(Guid id);
    }
}
