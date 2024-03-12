using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstract.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> options = null, Func<IQueryable<TEntity>,IIncludableQueryable<TEntity, object>> include=null);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> options = null, Func<IQueryable<TEntity>,IIncludableQueryable<TEntity, object>> include=null);
        Task<TEntity> CreateAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
