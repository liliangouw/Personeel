using Personeel.IDAL;
using Personeel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity,new()
    {
        //获取上下文
        private readonly PersoneelContext _db;
        public BaseService(Models.PersoneelContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(T model, bool saved = true)
        {
            _db.Set<T>().Add(model);
            if (saved)
            {
                await _db.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            //关闭校验
            _db.Configuration.ValidateOnSaveEnabled = false;
            //状态为修改
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }
        /// <summary>
        /// 返回所有未被删除的数据（没有真的执行）
        /// </summary>
        /// <returns></returns>
        public  IQueryable<T> GetAllAsync()
        {
            return _db.Set<T>().Where(m => !m.IsRemoved).AsNoTracking();
        }

        public  IQueryable<T> GetAllByOrderAsync(bool asc = true)
        {
            var data = GetAllAsync();
            data = asc ? data.OrderBy(m => m.CreateTime) : data.OrderByDescending(m => m.CreateTime);
            return data;
        }

        public  IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0)
        {
            return GetAllAsync().Skip(pageIndex * pageSize).Take(pageSize);
        }

        public  IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetAllByOrderAsync(asc).Skip(pageIndex * pageSize).Take(pageSize);
        }

        public async Task<T> GetOneByIdAsync(Guid id)
        {
           return await GetAllAsync().Where(m=>m.Id==id).FirstAsync();
        }

        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T(){Id = id};
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemoved = true;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
           await RemoveAsync(model.Id, saved);
           _db.Configuration.ValidateOnSaveEnabled = true;
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }

        public IQueryable<T> GetAllByStartEndTime(DateTime startTime, DateTime endTime)
        {
            return  GetAllByOrderAsync().Where(m => m.CreateTime>=startTime&&m.CreateTime<=endTime);
        }
        
    }
}
