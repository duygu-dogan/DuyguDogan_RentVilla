using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Cart;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.Concrete.Region;

namespace RentVilla.Persistance.Contexts
{
    public class RentVillaDbContext: IdentityDbContext<AppUser, AppRole, string>
    {

        public RentVillaDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<ProductAddress> ProductAddresses { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<Domain.Entities.Concrete.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<ReservationCart> ReservationCarts { get; set; }
        public DbSet<ReservationCartItem> ReservationCartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Reservation>()
            //    .HasKey(r => r.Id);
            //builder.Entity<ReservationCart>()
            //    .HasOne(rc => rc.Reservation)
            //    .WithOne(r => r.ReservationCart)
            //    .HasForeignKey<Reservation>(rc => rc.Id);
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                var _ = data.State switch
                {
                    EntityState.Added => data.CurrentValues[nameof(BaseEntity.CreatedAt)] = DateTime.UtcNow,
                    EntityState.Modified => data.CurrentValues[nameof(BaseEntity.UpdatedAt)] = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
