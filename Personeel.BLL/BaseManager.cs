using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DAL;
using Personeel.IBLL;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.BLL
{
    public class BaseManager
    {
        
        public static void AddOperation(Guid userId, string text)
        {
            IOperationService operationService = new OperationService();
             operationService.CreateAsync(new Operationlog()
            {
                Logdes = text,
                UserId = userId
            });
        }
    }
}
