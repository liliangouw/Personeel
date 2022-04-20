using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.DAL
{
    public class PayRollAccountService : BaseService<Models.PayRollAccount>, IPayRollAccountService
    {
        public PayRollAccountService() : base(new PersoneelContext())
        {
        }
    }
}
