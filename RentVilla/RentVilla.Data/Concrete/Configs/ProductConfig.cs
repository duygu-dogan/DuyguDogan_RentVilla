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
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.ImageUrl).IsRequired();
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
            builder.Property(p => p.UpdatedAt).HasDefaultValueSql("now()");
        }
    }
}
