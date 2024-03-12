using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentVilla.Domain.Entities.Concrete;

namespace RentVilla.Persistence.Configs
{
    public class ItemAttributeConfig : IEntityTypeConfiguration<ItemAttribute>
    {
        public void Configure(EntityTypeBuilder<ItemAttribute> builder)
        {
            builder.HasKey(i => i.Id);
        }
    }
}
