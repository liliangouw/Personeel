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
    public class AnnounceManager : IAnnounceManager
    {
        public async Task AddAnnounce(string title, string des, string text, Guid userId)
        {
            using (IAnnounceService announceService = new AnnounceService())
            {
                await announceService.CreateAsync(new Announce()
                {
                    AnnounceTitle = title,
                    AnnounceDes = des,
                    AnnounceText = text,
                    UserId = userId
                });
            }
        }

        public async Task<List<AnnounceInfoDto>> GetAllAnnounce()
        {
            using (IAnnounceService announceService = new AnnounceService())
            {
                return await announceService.GetAllAsync().Select(m => new DTO.AnnounceInfoDto()
                {
                    Id = m.Id,
                    Title = m.AnnounceTitle,
                    Des = m.AnnounceDes,
                    Text = m.AnnounceText,
                    Name = m.User.Name,
                    CreateTime = m.CreateTime
                }).ToListAsync();
            }
        }

        public async Task RemoveAnnounce(Guid id)
        {
            using (IAnnounceService announceService = new AnnounceService())
            {
                await announceService.RemoveAsync(id);
            }
        }

        public async Task EditAnnounce(Guid id, string title, string des, string text)
        {
            using (IAnnounceService announceService = new AnnounceService())
            {
                var regulatory = await announceService.GetAllAsync().FirstAsync(m => m.Id == id);
                regulatory.AnnounceTitle = title;
                regulatory.AnnounceDes = des;
                regulatory.AnnounceText = text;
                await announceService.EditAsync(regulatory);
            }
        }

        public async Task<AnnounceInfoDto> GetOneAnnounce(Guid id)
        {
            using (IAnnounceService announceService = new AnnounceService())
            {
                return await announceService.GetAllAsync()
                    .Include(m => m.User)
                    .Where(m => m.Id == id).Select(m => new AnnounceInfoDto()
                    {
                        CreateTime = m.CreateTime,
                        Des = m.AnnounceDes,
                        Id = m.Id,
                        Name = m.User.Name,
                        Text = m.AnnounceText,
                        Title = m.AnnounceTitle
                    }).FirstAsync();
            }
        }
    }

}
