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

        public async Task<List<OperationinfoDto>> GetAllOperation(int pageIndex, int pageSize)
        {
            using (IOperationService operationService = new OperationService())
            {
                return await operationService.GetAllByPageOrderAsync(pageSize,pageIndex,false).Select(m => new DTO.OperationinfoDto()
                {
                    CreateTime = m.CreateTime,
                    Id = m.Id,
                    Name = m.User.Name,
                    Text = m.Logdes
                }).ToListAsync();
            }
        }
        public async Task<List<DTO.OperationinfoDto>> GetAll()
        {
            using (IOperationService operationService = new OperationService())
            {
                return await operationService.GetAllByOrderAsync(false).Select(m => new DTO.OperationinfoDto()
                {
                    CreateTime = m.CreateTime,
                    Id = m.Id,
                    Name = m.User.Name,
                    Text = m.Logdes
                }).ToListAsync();
            }
        }
        public async Task<int> GetDataCount()
        {
            using(IDAL.IOperationService operationService=new OperationService())
            {
                return  await operationService.GetAllAsync().CountAsync();
            }
        }

        public async Task<OperationinfoDto> GetOperationById(Guid id)
        {
            using (IOperationService operationService = new OperationService())
            {
                return await operationService.GetAllAsync()
                    .Include(m =>m.User)
                    .Where(m => m.Id == id)
                    .Select(m => new OperationinfoDto()
                    {
                        CreateTime = m.CreateTime,
                        Id = m.Id,
                        Name = m.User.Name,
                        Text = m.Logdes
                    }).FirstAsync();
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
