using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface IOperationManager
    {
        Task AddOperation(Guid userId, string text);
        Task<List<DTO.OperationinfoDto> >GetAllOperation();
        Task<OperationinfoDto> GetOperationById(Guid id,string name);
        Task RemoveOperation(Guid id);
    }
}
