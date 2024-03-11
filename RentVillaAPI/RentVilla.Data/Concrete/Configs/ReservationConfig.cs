using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentVilla.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Data.Concrete.Configs
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.CreatedDate).HasDefaultValueSql("now()").IsRequired();
            builder.Property(r => r.StartDate).IsRequired();
            builder.Property(r => r.EndDate).IsRequired();
            builder.Property(r => r.TotalCost).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(r => r.ProductPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(r => r.AddServiceCost).HasColumnType("decimal(18,2)");
        }
    }
}
