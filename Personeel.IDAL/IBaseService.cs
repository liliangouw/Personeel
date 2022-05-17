using Personeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.IDAL
{
    public interface IBaseService<T>:IDisposable where T:BaseEntity
    {
        //默认自动保存
        Task CreateAsync(T model,bool saved=true);
        Task EditAsync(T model, bool saved = true);
        Task RemoveAsync(Guid id, bool saved = true);
        Task RemoveAsync(T model, bool saved = true);
        Task Save();
        Task<T> GetOneByIdAsync(Guid id);
        IQueryable<T> GetAllAsync();
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0);
        /// <summary>
        /// 排序，true为升序，false为降序 
        /// </summary>
        /// <param name="asc"></param>
        /// <returns></returns>
        IQueryable<T> GetAllByOrderAsync(bool asc=true);
        IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0,bool asc=true);

        IQueryable<T> GetAllByStartEndTime(DateTime startTime, DateTime endTime);
    }
}
