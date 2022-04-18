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
        //薪酬管理
        Task AddSalary(Guid userId, int basicMoney, int encourageOrChastisement, int shouldDays, int actualDays,
            int subsidies, int accumulationfund, int socialSecurity, int tax, string month);

        Task<SalaryInfoDto> GetInfoById(Guid id);
        Task<SalaryInfoDto> GetInfoByUserId(Guid id);
        Task<List<SalaryInfoDto>> GetAllSalary();
        Task<List<SalaryInfoDto>> GetAllSalaryByUserId(Guid userGuid);

        Task EditSalary(Guid id, int basicMoney, int encourageOrChastisement, int shouldDays, int actualDays,
            int subsidies, int accumulationfund, int socialSecurity, int tax, string month);

        Task Remove(Guid id);

        //调薪管理
        Task<List<UserInfoDto>> GetAllUserBasicSalary();
        Task EditUserBasicSalary(Guid userGuid, string Reason, int basicSalary);
        Task<UserInfoDto> GetOneBasicSalary(Guid userGuid);
        
        //调薪记录管理
        Task<List<ChangeSalaryInfoDto>> GetAllChangeRecord();
        Task<List<ChangeSalaryInfoDto>> GetRecordByUserId(Guid id);
        Task RemoveRecord(Guid id);

    }
}
