using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Repositories
{
    public interface IReadRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(bool tracking = true);
        List<TEntity> GetAllList();
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> options, bool tracking = true);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> options, bool tracking = true);
        Task<TEntity> GetByIdAsync(string id, bool tracking = true);
    }
}
