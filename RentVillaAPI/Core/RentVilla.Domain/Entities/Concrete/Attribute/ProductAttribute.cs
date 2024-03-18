using RentVilla.Domain.Entities.Abstract;

namespace RentVilla.Domain.Entities.Concrete.Attribute
{
    public class ProductAttribute : BaseEntity
    {
        public AttributeType AttributeType { get; set; }
        public Attributes Attributes { get; set; }
        public Product Product { get; set; }
    }
}
