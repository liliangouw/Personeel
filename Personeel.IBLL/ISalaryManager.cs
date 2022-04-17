using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DTO;

namespace Personeel.IBLL
{
    public interface ISalaryManager
    {
        Task AddSalary(Guid userId,int basicMoney, int encourageOrChastisement, int shouldDays, int actualDays, int subsidies, int accumulationfund, int socialSecurity, int tax,string month);
        Task<SalaryInfoDto> GetInfoById(Guid id);
        Task<SalaryInfoDto> GetInfoByUserId(Guid id);
        Task<List<SalaryInfoDto>> GetAllSalary();
        Task<List<SalaryInfoDto>> GetAllSalaryByUserId(Guid userGuid);
        Task Remove(Guid id);
    }
}
