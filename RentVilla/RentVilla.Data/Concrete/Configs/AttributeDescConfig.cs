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
    public class AttributeDescConfig : IEntityTypeConfiguration<AttributeDesc>
    {
        public void Configure(EntityTypeBuilder<AttributeDesc> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();


        }
    }
}
