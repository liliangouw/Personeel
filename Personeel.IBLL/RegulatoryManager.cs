﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DAL;
using Personeel.DTO;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.IBLL
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
