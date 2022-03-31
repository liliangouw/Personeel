using Personeel.IDAL;
using Personeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DAL
{
    public class UserService : BaseService<Models.User>, IUserService
    {
        public UserService() : base(new PersoneelContext())
        {
        }
    }
}
