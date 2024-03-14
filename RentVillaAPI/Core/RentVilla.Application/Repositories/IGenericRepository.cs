using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RentVilla.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> AppDbContext { get; }
    }
}
