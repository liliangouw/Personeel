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
    public class DepartmentManager : IDepartmentManager
    {
        public async Task AddDep(string depName, string depDes)
        {
            using(IDepartmentService departmentService=new DepartmentService())
            {
                await departmentService.CreateAsync(new Department()
                {
                    Depname = depName,
                    Depdescribe = depDes
                });
            }
        }

        public async Task EditDep(string depName, string depDes)
        {
            using(IDepartmentService departmentService=new DepartmentService())
            {
                var Dep = await departmentService.GetAllAsync().FirstAsync(m => m.Depname == depName);
                Dep.Depname = depName;
                Dep.Depdescribe = depDes;
                await departmentService.EditAsync(Dep);
            }
        }

        public async Task<List<DepInfoDto>> GetInfo()
        {
            using (IDepartmentService departmentService = new DepartmentService())
            {
                return await departmentService.GetAllAsync().Select(m => new DTO.DepInfoDto()
                {
                    DepName=m.Depname,
                    DepDes=m.Depdescribe
                }).ToListAsync();

            }
        }

        public async Task RemoveDep(string depName)
        {
            using(IDepartmentService departmentService=new DepartmentService())
            {
                var Dep = await departmentService.GetAllAsync().FirstAsync(m => m.Depname == depName);
                Dep.IsRemoved = false;
                await departmentService.EditAsync(Dep);
            }
            

        }
    }
}
