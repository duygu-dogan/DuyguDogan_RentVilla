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
    public class AttributeDescConfig : IEntityTypeConfiguration<AttributeDesc>
    {
        public void Configure(EntityTypeBuilder<AttributeDesc> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Description).HasMaxLength(100).IsRequired();


        }
    }
}
