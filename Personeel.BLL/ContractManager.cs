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
    public class ContractManager:IContractManager
    {
        public async Task<List<ContractInfoDto>> GetAll()
        {
            using (IContractService contractService = new ContractService())
            {
                return await contractService.GetAllAsync().Include(m => m.User).Select(m => new ContractInfoDto()
                {
                    Id = m.Id,
                    UserGuid = m.UserGuid,
                    UserName = m.User.Name,
                    DepName = m.User.Department.Depname,
                    Years = m.Years,
                    CreateTime = m.CreateTime,
                    DeadLine = m.DeadLine,
                    FilePath = m.FilePath
                }).ToListAsync();
            }
        }

        public async Task Add(Guid userGuid, DateTime startTime, int year, string filePath)
        {
            using (IContractService contractService = new ContractService())
            {
               await contractService.CreateAsync(new Contract()
                {
                    StartTime = startTime,
                    UserGuid = userGuid,
                    DeadLine = DateTime.Now.AddYears(year),
                    Years = year,
                    FilePath = filePath
                });
            }
        }

        public async Task<ContractInfoDto> GetOneById(Guid id)
        {
            using (IContractService contractService = new ContractService())
            {
                return await contractService.GetAllAsync().Include(m => m.User).Include(m=>m.User.Department).Where(m=>m.Id==id).Select(m => new ContractInfoDto()
                {
                    Id = m.Id,
                    UserGuid = m.UserGuid,
                    UserName = m.User.Name,
                    DepName = m.User.Department.Depname,
                    Years = m.Years,
                    CreateTime = m.StartTime,
                    DeadLine = m.DeadLine,
                    FilePath = m.FilePath
                }).FirstAsync();
            }
        }

        public async Task Edit(Guid id,DateTime startTime, int year, string filePath)
        {
            using (IContractService contractService = new ContractService())
            {
                Contract info = await contractService.GetOneByIdAsync(id);
                info.StartTime = startTime;
                info.DeadLine = startTime.AddYears(year);
                info.Years = year;
                info.FilePath = filePath;
                await contractService.EditAsync(info);
            }
        }

        public async Task Remove(Guid id)
        {
            using (IContractService contractService = new ContractService())
            {
                await contractService.RemoveAsync(id);
            }
        }
    }
}
