using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.DAL
{
    public class OperationService:BaseService<Operationlog>,IOperationService
    {
        public OperationService() : base( new PersoneelContext())
        {
        }
    }
}
