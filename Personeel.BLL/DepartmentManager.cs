using Personeel.DAL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.Models;
using System.Data.Entity;

namespace Personeel.BLL
{
    public class DepartmentManager :IDepartmentManager
    {
        public async Task AddDep(string depName, string depDes,Guid depUser)
        {
            using(IDepartmentService departmentService=new DepartmentService())
            {
                await departmentService.CreateAsync(new Department()
                {
                    Depname = depName,
                    Depdescribe = depDes,
                    UserGuid = depUser
                });
            }
        }

        public async Task EditDep(string depName, string depDes,Guid depUser)
        {
            using(IDepartmentService departmentService=new DepartmentService())
            {
                var Dep = await departmentService.GetAllAsync().FirstAsync(m => m.Depname == depName);
                Dep.Depname = depName;
                Dep.Depdescribe = depDes;
                Dep.UserGuid = depUser;
                await departmentService.EditAsync(Dep);
            }
        }

        public async Task<List<DepInfoDto>> GetInfo()
        {
            using (IDepartmentService departmentService = new DepartmentService())
            {
                return await departmentService.GetAllAsync().Select(m => new DTO.DepInfoDto()
                {
                    DepGuid = m.Id,
                    DepName=m.Depname,
                    DepDes=m.Depdescribe,
                    DepUserGuid = m.UserGuid
                }).ToListAsync();

            }
        }

        public async Task<DepInfoDto> GetInfoById(Guid id)
        {
            using (IDepartmentService departmentService = new DepartmentService())
            {
                    return await departmentService.GetAllAsync()
                    .Where(m => m.Id == id).Select(m => new DepInfoDto()
                    {
                        DepGuid = m.Id,
                        DepName = m.Depname,
                        DepDes = m.Depdescribe,
                        DepUserGuid = m.UserGuid
                    }).FirstAsync();
            }
        }

        public async Task<DepInfoDto> GetInfoByName(string depName)
        {
            using (IDepartmentService departmentService = new DepartmentService())
            {
                return await departmentService.GetAllAsync()
                    .Where(m => m.Depname == depName).Select(m => new DepInfoDto()
                    {
                        DepGuid = m.Id,
                        DepName = m.Depname,
                        DepDes = m.Depdescribe,
                        DepUserGuid = m.UserGuid
                    }).FirstAsync();

            }
        }

        public async Task RemoveDep(Guid id)
        {
            using(IDepartmentService departmentService=new DepartmentService())
            {
                await departmentService.RemoveAsync(id);
            }
            

        }
    }
}
