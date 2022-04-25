using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.DAL
{
    public class ContractService : BaseService<Contract>, IContractService
    {
        public ContractService() : base(new PersoneelContext())
        {
        }
    }
}
