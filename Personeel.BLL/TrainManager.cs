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
    internal class TrainManager : ITrainManager
    {
        public async Task AddTrain(string trainSort, string trainDes, DateTime startTime, DateTime endTime,
            Guid userGuid)
        {
            using (ITrainService trainService = new TrainService())
            {
                await trainService.CreateAsync(new Train()
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    TrainSort = trainSort,
                    TrainDes = trainDes,
                    UserId = userGuid
                });
            }
        }

        public async Task<List<TrainInfoDto>> GetAllTrain()
        {
            using (ITrainService trainService = new TrainService())
            {
                return await trainService.GetAllAsync().Select(m => new DTO.TrainInfoDto()
                {
                    EndTime = m.EndTime,
                    StartTime = m.StartTime,
                    TrainSort = m.TrainSort,
                    TrainDes = m.TrainDes,
                    TrainGuid = m.Id,
                    UserId = m.UserId
                }).ToListAsync();
            }
        }

        public async Task<TrainInfoDto> GetOneById(Guid id)
        {
            using (ITrainService trainService = new TrainService())
            {
                return await trainService.GetAllAsync().Include(m => m.User)
                    .Where(m => m.Id == id)
                    .Select(m =>
                        new DTO.TrainInfoDto()
                        {
                            TrainGuid = m.Id,
                            TrainSort = m.TrainSort,
                            TrainDes = m.TrainDes,
                            TrainResult = m.TrainResult,
                            TrainComment = m.TarinComment,
                            StartTime = m.StartTime,
                            EndTime = m.EndTime,
                            UserName = m.User.Name
                        }).FirstAsync();
            }
        }

        public async Task EditTrain(Guid id, string trainResult, string trainComment)
        {
            using (ITrainService trainService = new TrainService())
            {
                Train train = await trainService.GetAllAsync().Where(m => m.Id == id).FirstAsync();
                train.TrainResult = trainResult;
                train.TarinComment = trainComment;
                await trainService.EditAsync(train);
            }
        }

        public async Task RemoveTrain(Guid id)
        {
            using (ITrainService trainService = new TrainService())
            {
               await trainService.RemoveAsync(id);
            }
        }
    }
}
