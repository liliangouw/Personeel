using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.IBLL
{
    public interface IDepartmentManager
    {
        Task AddDep(string depName, string depDes);
        Task EditDep(string depName, string depDes);
        Task RemoveDep(string depName);
        Task<List<DTO.DepInfoDto>>GetInfo();
    }
}
