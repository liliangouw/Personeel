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

        public async Task ChangeInfo(Guid id,string posName, string posDescribe)
        {
            using (IDAL.IPositionService iPositionService=new DAL.PositionService())
            {
                var user = await iPositionService.GetAllAsync().FirstAsync(m => m.Id == id);
                user.Posname = posName;
                user.Posdescribe = posDescribe;
                await iPositionService.EditAsync(user);
            }
        }

        public async Task RemovePosition(Guid id)
        {
            using (IDAL.IPositionService iPositionService = new DAL.PositionService())
            {
                await iPositionService.RemoveAsync(id);
            }
        }
        
        public async  Task<List<PositionInfoDto>> GetInfo()
        {
            using (IDAL.IPositionService iPositionService=new DAL.PositionService())
            {
                return await  iPositionService.GetAllByOrderAsync().Select(m => new DTO.PositionInfoDto()
                {
                    PositionGuid = m.Id,
                    PositionName = m.Posname,
                    PositionDescribe = m.Posdescribe
                }).ToListAsync();
                
            }
        }

        public async Task<PositionInfoDto> GetInfoById(Guid id)
        {
            using (IPositionService positionService=new PositionService())
            {
                Position positionModel = await positionService.GetOneByIdAsync(id);
                DTO.PositionInfoDto position = new PositionInfoDto()
                {
                    PositionGuid = positionModel.Id,
                    PositionName = positionModel.Posname,
                    PositionDescribe = positionModel.Posdescribe
                };
                return position;
            }
        }

        public async Task<PositionInfoDto> GetInfoByName(string posName)
        {
            using (IPositionService positionService = new PositionService())
            {
                Position positionModel = await positionService.GetAllAsync().Where(m=>m.Posname==posName).FirstAsync();
                DTO.PositionInfoDto position = new PositionInfoDto()
                {
                    PositionGuid = positionModel.Id,
                    PositionName = positionModel.Posname,
                    PositionDescribe = positionModel.Posdescribe
                };
                return position;
            }
        }
    }
}
