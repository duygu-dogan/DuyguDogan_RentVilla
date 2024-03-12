using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Configs
{
    public class AddOnsConfig : IEntityTypeConfiguration<AddOns>
    {
        public void Configure(EntityTypeBuilder<AddOns> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Price).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
