using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RentVilla.Persistance.Contexts;
using RentVilla.Persistence.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RentVillaDbContext>
    {
        //public RentVillaDbContext CreateDbContext(string[] args)                    
        //{   DbContextOptionsBuilder<RentVillaDbContext> dbContextOptionsBuilder = new();
        //    dbContextOptionsBuilder.UseNpgsql(Configuration.GetConnectionString);
        //    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        //    return new (dbContextOptionsBuilder.Options);
        //}
        public RentVillaDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<RentVillaDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlite(Configuration.GetConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
