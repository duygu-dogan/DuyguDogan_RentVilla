using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstract.Repositories
{
    public interface IWriteRepository<TEntity>: IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(List<TEntity> entities);
        bool Update(TEntity entity);
        Task<bool> DeleteAsync(string id);
        bool Delete(TEntity entity);
        bool DeleteRange(List<TEntity> entities);
        Task<int> SaveAsync();
    }
}
