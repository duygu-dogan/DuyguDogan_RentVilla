using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentVilla.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Data.Concrete.Contexts
{
    public class RentVillaDbContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public RentVillaDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<AttributeDesc> AttributeDescs { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
        public DbSet<ItemAttribute> ItemAttributes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Product).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection"));
        }

    }
}
