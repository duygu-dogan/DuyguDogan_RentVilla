using RentVilla.Domain.Entities.Abstract;

namespace RentVilla.Domain.Entities.Concrete.Attribute
{
    public class Attributes
    {
        public Guid Id { get; set; }
        public AttributeType AttributeType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
