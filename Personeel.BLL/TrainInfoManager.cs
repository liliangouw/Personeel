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
    public class TrainInfoManager:ITrainInfoManager
    {
        public async Task<List<TrainInfoInfoDto>> GetAll()
        {
            using (ITrainInfoService trainInfoService=new TrainInfoService())
            {
                return await trainInfoService.GetAllAsync().Select(m => new TrainInfoInfoDto()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Sort = m.Sort,
                    CreateTime = m.CreateTime,
                    FilePath = m.FilePath
                }).ToListAsync();
            }
        }

        public async Task Add(string title, string sort, string filePath)
        {
            using (ITrainInfoService trainInfoService=new TrainInfoService())
            {
                await trainInfoService.CreateAsync(new TrainInfo()
                {
                    Title = title,
                    Sort = sort,
                    FilePath = filePath
                });
            }
        }

        public async Task<TrainInfoInfoDto> GetOneById(Guid id)
        {
            using (ITrainInfoService trainInfoService=new TrainInfoService())
            {
                return await trainInfoService.GetAllAsync().Where(m => m.Id == id).Select(m => new TrainInfoInfoDto()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Sort = m.Sort,
                    FilePath = m.FilePath
                }).FirstAsync();
            }
        }

        public async Task Edit(Guid id, string title, string sort, string filePath)
        {
            using (ITrainInfoService trainInfoService=new TrainInfoService())
            {
                TrainInfo info = await trainInfoService.GetOneByIdAsync(id);
                info.Title = title;
                info.Sort = sort;
                info.FilePath = filePath;
                await trainInfoService.EditAsync(info);
            }
        }

        public async Task Remove(Guid id)
        {
            using (ITrainInfoService trainInfoService=new TrainInfoService())
            {
                await trainInfoService.RemoveAsync(id);
            }
        }
    }
}
