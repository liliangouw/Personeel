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
    public class AssessManager:IAssessManager
    {
        public async Task Add(string name, string type, List<Guid> userGuid)
        {
            using (IAssessService assessService=new AssessService())
            {
                foreach (var item in userGuid)
                {
                    await assessService.CreateAsync(new Assess()
                    {
                        AssessName = name,
                        AssessType = type,
                        UserGuid = item,
                    });
                }
            }
        }

        public async Task<List<AssessInfoDto>> GetAll()
        {
            using (IAssessService assessService = new AssessService())
            {
                List<Assess> info= await assessService.GetAllByOrderAsync(false).Include(m=>m.User).Include(m=>m.User.Department).ToListAsync();
                List<AssessInfoDto> list = info.Select(m => new AssessInfoDto()
                {
                    AssessName = m.AssessName,
                    AssessType = m.AssessType,
                    Id = m.Id,
                    Result = m.Result,
                    CreateTime = m.CreateTime,
                    State = Enum.GetName(typeof(State), m.State),
                    UserDep = m.User.Department.Depname,
                    UserName = m.User.Name
                }).ToList();
                return list;
            }
        }

        public async Task<AssessInfoDto> GetOneById(Guid id)
        {
            using (IAssessService assessService = new AssessService())
            {
                var info = await assessService.GetAllAsync().Include(m=>m.User).Include(m=>m.User.Department).Where(m=>m.Id==id).FirstAsync();
                AssessInfoDto list = new AssessInfoDto()
                {
                    AssessName = info.AssessName,
                    AssessType = info.AssessType,
                    Id = info.Id,
                    Result = info.Result,
                    CreateTime = info.CreateTime,
                    State = Enum.GetName(typeof(State), info.State),
                    UserDep = info.User.Department.Depname,
                    UserName = info.User.Name
                };
                return list;
            }
            
        }

        public async Task Edit(Guid id, int grade, int state)
        {
            using (IAssessService assessService = new AssessService())
            {

                string result = "";
                string a = grade.ToString();
                if (grade >= 85)
                {
                    result = "A/";
                }
                else if(75<=grade&&grade<85)
                {
                    result = "B/";
                }
                else if(60<=grade&&grade<75)
                {
                    result = "C/";
                }
                else
                {
                    result = "D/";
                }

                result += a;
                var info = await assessService.GetOneByIdAsync(id);
                info.Result = result;
                info.State = state;
                await assessService.EditAsync(info);
            }
        }

        public async Task Remove(Guid id)
        {
            using (IAssessService assessService = new AssessService())
            {
                await assessService.RemoveAsync(id);
            }
        }

        public async Task<List<AssessInfoDto>> GetAllByUserId(Guid userGuid)
        {
            using (IAssessService assessService=new AssessService())
            {
                var info = await assessService.GetAllAsync().Where(m=>m.UserGuid==userGuid).ToListAsync();
                List<AssessInfoDto> list = info.Select(m => new AssessInfoDto()
                {
                    AssessName = m.AssessName,
                    AssessType = m.AssessType,
                    Id = m.Id,
                    Result = m.Result,
                    CreateTime = m.CreateTime,
                    State = Enum.GetName(typeof(State), m.State),
                    UserDep = m.User.Department.Depname,
                    UserName = m.User.Name
                }).ToList();
                return list;
            }
        }
    }
}
