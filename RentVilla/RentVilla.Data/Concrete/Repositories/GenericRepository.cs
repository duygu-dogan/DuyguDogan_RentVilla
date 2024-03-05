using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RentVilla.Data.Abstract;
using RentVilla.Data.Concrete.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Data.Concrete.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> options = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if(include != null)
            {
                query = include(query);
            }
            if(options != null)
            {
                query = query.Where(options);
            }
            return await query.ToListAsync();
        }

        public Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> options = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (include != null)
            {
                query = include(query);
            }
            if (options != null)
            {
                query = query.Where(options);
            }
            return query.FirstOrDefaultAsync();
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
