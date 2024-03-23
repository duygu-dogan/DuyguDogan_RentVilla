using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Repositories;
using RentVilla.Domain.Entities.Abstract;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        protected readonly RentVillaDbContext _context;

        public ReadRepository(RentVillaDbContext context)
        {
            _context = context;
        }
        public DbSet<TEntity> AppDbContext => _context.Set<TEntity>();

        public IQueryable<TEntity> GetAll(bool tracking = true)
        {
            var query = AppDbContext.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        public List<TEntity> GetAllList()
        {
            var entities = AppDbContext.ToList();
            return entities;
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> options, bool tracking = true)
        {
            var query = AppDbContext.Where(options);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> options, bool tracking = true)
        {
            var query = AppDbContext.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.SingleOrDefaultAsync(options);

        }

        public async Task<TEntity> GetByIdAsync(string id, bool tracking = true)
        {
            TEntity entity = await AppDbContext.FindAsync(Guid.Parse(id));
            return entity;
        }
    }
}
