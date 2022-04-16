using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DAL;
using Personeel.DTO;
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

        public async Task<List<EncourageInfoDto>> GetAll()
        {
            using (IEncourageService encourageService = new EncourageService())
            {
                return  await encourageService.GetAllAsync().Select(m => new EncourageInfoDto()
                {
                    Category = m.Category,
                    Money = m.Money,
                    Reason = m.Reason,
                    UserId = m.UserId,
                    UserName = m.User.Name,
                    Id = m.Id
                }).ToListAsync();
            }
        }

        public async Task<EncourageInfoDto> GetOneById(Guid id)
        {
            using (IEncourageService encourageService = new EncourageService())
            {
               return await encourageService.GetAllAsync().Include(m => m.User).Where(m => m.Id == id).Select(m =>
                    new EncourageInfoDto()
                    {
                        Category = m.Category,
                        Money = m.Money,
                        Reason = m.Reason,
                        UserId = m.UserId,
                        UserName = m.User.Name,
                        Id = m.Id
                    }).FirstAsync();
            }
        }

        public async Task Edit(Guid id, string category, string reason, int money)
        {
            using (IEncourageService encourageService = new EncourageService())
            {
               var info=await encourageService.GetAllAsync().Where(m => m.Id == id).FirstAsync();
               info.Category = category;
               info.Reason = reason;
               info.Money = money;
               await encourageService.EditAsync(info);
            }
        }

        public async Task Remove(Guid id)
        {
            using (IEncourageService encourageService = new EncourageService())
            {
                await encourageService.RemoveAsync(id);
            }
        }
    }
}
