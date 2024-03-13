using Microsoft.EntityFrameworkCore;
using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistance.Contexts
{
    public class RentVillaDbContext: DbContext
    {

        public RentVillaDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<AttributeDesc> AttributeDescs { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
        public DbSet<ItemAttribute> ItemAttributes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
