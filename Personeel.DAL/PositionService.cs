using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.DAL
{
    public class PositionService : BaseService<Position>, IPositionService
    {
        public PositionService() : base(new PersoneelContext())
        {
        }
    }
}
