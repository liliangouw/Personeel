using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public class RegulatoryManager:IRegulatoryManager
    {
        public async Task AddRegulatory(string title, string des, string text, Guid userId)
        {
            using (IRegulatoryService referenceService = new RegulatoryService())
            {
               await referenceService.CreateAsync(new Regulatory()
                {
                    Title = title,
                    RoleDes = des,
                    RoleText = text,
                    UserId = userId
                });
            }
        }

        public async Task<List<RegulatoryInfoDto>> GetAllRegulatory()
        {
            using (IDAL.IRegulatoryService regulatoryService = new DAL.RegulatoryService())
            {
                return await regulatoryService.GetAllAsync().Select(m => new DTO.RegulatoryInfoDto()
                  {
                    Id = m.Id,
                    Title = m.Title,
                    Des = m.RoleDes,
                    Text = m.RoleText,
                    Name = m.User.Name,
                    CreateTime = m.CreateTime
                  }).ToListAsync();
            }
        }
        public async Task<RegulatoryInfoDto> GetOneRegulatory(Guid id,string name)
        {
            using (IDAL.IRegulatoryService regulatoryService = new DAL.RegulatoryService())
            {
                var regulatory = await regulatoryService.GetOneByIdAsync(id);
                RegulatoryInfoDto info = new RegulatoryInfoDto()
                {
                    Id = regulatory.Id,
                    CreateTime = regulatory.CreateTime,
                    Des = regulatory.RoleDes,
                    Title = regulatory.Title,
                    Name = name,
                    Text = regulatory.RoleText
                };
                return info;
            }
        }

        public async Task RemoveRegulatory(Guid id)
        {
            using (IRegulatoryService regulatoryService = new RegulatoryService())
            {
               await  regulatoryService.RemoveAsync(id);
            }
        }

        public async Task EditRegulatory(Guid id, string title, string des,string text)
        {
            using (IRegulatoryService regulatoryService = new RegulatoryService())
            {
               var regulatory= await regulatoryService.GetAllAsync().FirstAsync(m => m.Id == id);
               regulatory.Title = title;
               regulatory.RoleDes = des;
               regulatory.RoleText = text;
               await regulatoryService.EditAsync(regulatory);
            }
        }
    }
}
