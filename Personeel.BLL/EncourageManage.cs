using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DAL;
using Personeel.IBLL;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.BLL
{
    public class EncourageManage:IEncourageManager
    {
        public async Task AddEncourage(Guid userGuid, string category, string reason, int money)
        {
            using (IEncourageService encourageService = new EncourageService())
            {
               await  encourageService.CreateAsync(new EncourageOrChastisement()
                {
                    Category = category,
                    Reason = reason,
                    UserId = userGuid,
                    Money = money
                });
            }
        }

        public async Task GetAll()
        {
            using (IEncourageService encourageService = new EncourageService())
            {
                
            }
        }

        public async Task GetOneById(Guid id)
        {
            using (IEncourageService encourageService = new EncourageService())
            {

            }
        }

        public async Task Edit(Guid userGuid, string category, string reason, int money)
        {
            using (IEncourageService encourageService = new EncourageService())
            {

            }
        }

        public async Task Remove(Guid id)
        {
            using (IEncourageService encourageService = new EncourageService())
            {

            }
        }
    }
}
