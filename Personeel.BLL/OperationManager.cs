using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DAL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.BLL
{
    public class OperationManager:IOperationManager
    {
        public async Task AddOperation(Guid userId, string text)
        {
            using (IOperationService operationService = new OperationService())
            { 
                await operationService.CreateAsync(new Operationlog()
                {
                  Logdes = text,
                  UserId = userId
                 });
            }
        }

        public async Task<List<OperationinfoDto>> GetAllOperation()
        {
            using (IOperationService operationService = new OperationService())
            {
                return await operationService.GetAllAsync().Select(m => new DTO.OperationinfoDto()
                {
                    CreateTime = m.CreateTime,
                    Id = m.Id,
                    Name = m.User.Name,
                    Text = m.Logdes
                }).ToListAsync();
            }
        }

        public async Task<OperationinfoDto> GetOperationById(Guid id,string name)
        {
            using (IOperationService operationService = new OperationService())
            {
                var operationInfo = await operationService.GetOneByIdAsync(id);
                OperationinfoDto operationInfoDto = new OperationinfoDto()
                {
                    CreateTime = operationInfo.CreateTime,
                    Id = operationInfo.Id,
                    Name = name,
                    Text = operationInfo.Logdes
                };
                return operationInfoDto;
            }
        }

        public async Task RemoveOperation(Guid id)
        {
            using (IOperationService operationService = new OperationService())
            {
                await operationService.RemoveAsync(id);
            }
        }
    }
}
