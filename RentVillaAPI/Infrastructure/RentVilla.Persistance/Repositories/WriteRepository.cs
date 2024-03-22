using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RentVilla.Application.Repositories;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
    {
        private readonly RentVillaDbContext _context;

        public WriteRepository(RentVillaDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> AppDbContext => _context.Set<TEntity>();

        public async Task<bool> AddAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = await AppDbContext.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }
        public bool Add(TEntity entity)
        {
            var entityEntry = AppDbContext.Add(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<TEntity> entities)
        {
            await AppDbContext.AddRangeAsync(entities);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            TEntity entity = await AppDbContext.FindAsync(id);
            return Delete(entity);
        }

        public bool Delete(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = AppDbContext.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool DeleteRange(List<TEntity> entities)
        {
            AppDbContext.RemoveRange(entities);
            return true;
        }

        public async Task<int> SaveAsync()
         => await _context.SaveChangesAsync();

        public bool Update(TEntity entity)
        {
            EntityEntry entityEntry = AppDbContext.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
