using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.DAL
{
    public class EncourageService : BaseService<EncourageOrChastisement>, IEncourageService
    {
        public EncourageService() : base(new PersoneelContext())
        {
        }
    }
}
