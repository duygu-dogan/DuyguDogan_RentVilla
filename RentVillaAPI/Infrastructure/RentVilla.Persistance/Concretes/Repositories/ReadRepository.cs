using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Abstract.Repositories;
using RentVilla.Domain.Entities.Abstract;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Concretes.Repositories
{
    internal class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        private readonly RentVillaDbContext _context;

        public ReadRepository(RentVillaDbContext context)
        {
            _context = context;
        }
        public DbSet<TEntity> AppDbContext => _context.Set<TEntity>();

        public IQueryable<TEntity> GetAll()
            => AppDbContext;

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> options)
         => AppDbContext.Where(options);

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> options)
        => await AppDbContext.FirstOrDefaultAsync(options);

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            TEntity entity = await AppDbContext.FindAsync(id);
            return entity;
        }
    }
}
