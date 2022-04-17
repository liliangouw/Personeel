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
    public class CheckingManager:ICheckingManager
    {
        public async Task Begin(Guid userGuid)
        {
            using (ICheckingService checkingService = new CheckingService())
            {
                //判断打卡状态
                string status;
                DateTime dt=DateTime.Now;
                if (dt.Hour<=8||(dt.Hour <= 8 && dt.Minute <= 30))
                {
                    status = "正常";
                }
                else
                {
                    status = "异常";
                }
                DateTime createTime=DateTime.Now;
                await checkingService.CreateAsync(new Checking()
                {
                    UserGuid = userGuid,
                    BeginTime = createTime,
                    Status = status
                });
            }
        }

        public async Task End(Guid userid)
        {
            using (ICheckingService checkingService = new CheckingService())
            {
                string day = DateTime.Now.ToString("yyyy-MM-dd");
                Checking info = await checkingService.GetAllAsync().Where(m => m.DayTime ==day  && m.UserGuid == userid).FirstAsync();
                if (info.Status == "正常")
                {
                    DateTime dt = DateTime.Now;
                    if (dt.Hour <= 17 || (dt.Hour <= 17&& dt.Minute < 30))
                    {
                        info.Status = "异常";
                    }
                }
                info.EndTime = DateTime.Now;
              await checkingService.EditAsync(info);
            }
        }

        public async Task<List<CheckingInfoDto>> GetAllChecking()
        {
            using (ICheckingService checkingService = new CheckingService())
            {
                return await checkingService.GetAllAsync().Select(m => new CheckingInfoDto()
                {
                    Id = m.Id,
                    UserName = m.User.Name,
                    UserGuid = m.UserGuid,
                    BeginTime = m.BeginTime,
                    EndTime = m.EndTime,
                    DayTime = m.DayTime,
                    Status = m.Status
                }).ToListAsync();
            }
        }

        public async Task<List<CheckingInfoDto>> GetCheckingByUserId(Guid userGuid)
        {
            using (ICheckingService checkingService = new CheckingService())
            {
                return await checkingService.GetAllAsync().Where(m=>m.UserGuid==userGuid).Select(m => new CheckingInfoDto()
                {
                    Id = m.Id,
                    UserName = m.User.Name,
                    UserGuid = m.UserGuid,
                    BeginTime = m.BeginTime,
                    EndTime = m.EndTime,
                    DayTime = m.DayTime,
                    Status = m.Status
                }).ToListAsync();
            }
        }

        public async Task<List<CheckingInfoDto>> GetCheckingByDate(string day)
        {
            using (ICheckingService checkingService = new CheckingService())
            {
                return await checkingService.GetAllAsync().Where(m => m.DayTime==day).Select(m => new CheckingInfoDto()
                {
                    Id = m.Id,
                    UserName = m.User.Name,
                    UserGuid = m.UserGuid,
                    BeginTime = m.BeginTime,
                    EndTime = m.EndTime,
                    DayTime = m.DayTime,
                    Status = m.Status
                }).ToListAsync();
            }
        }

        public async Task RemoveChecking(Guid id)
        {
            using (ICheckingService checkingService = new CheckingService())
            {
               await checkingService.RemoveAsync(id);
            }
        }
    }
}
