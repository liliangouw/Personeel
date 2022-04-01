using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Personeel.DAL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.BLL
{
    public class PositionManager:IPositionManager
    {
        public async Task AddPosition(string posName, string posDescribe)
        {
            using (IDAL.IPositionService iPositionService=new DAL.PositionService())
            {
                await iPositionService.CreateAsync(new Position()
                {
                    Posname = posName,
                    Posdescribe = posDescribe
                });
            }
        }

        public async Task ChangeInfo(string posName, string posDescribe)
        {
            using (IDAL.IPositionService iPositionService=new DAL.PositionService())
            {
                var user = await iPositionService.GetAllAsync().FirstAsync(m => m.Posname == posName);
                user.Posname = posName;
                user.Posdescribe = posDescribe;
                await iPositionService.EditAsync(user);
            }
        }

        public async Task RemovePosition(string posName)
        {
            using (IDAL.IPositionService iPositionService = new DAL.PositionService())
            {
                var user = await iPositionService.GetAllAsync().FirstAsync(m => m.Posname == posName);
                user.IsRemoved = false;
                await iPositionService.EditAsync(user);
            }
        }
        //获取职位列表
        public async Task<PositionInfoDto> GetInfo()
        {
            using (IDAL.IPositionService iPositionService=new DAL.PositionService())
            {
                return  iPositionService.GetAllAsync().Select(m => new DTO.PositionInfoDto()
                {
                    PositionName = m.Posname,
                    PositionDescribe = m.Posdescribe
                });
                
            }
        }
    }
}
