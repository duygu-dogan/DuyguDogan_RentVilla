using RentVilla.Domain.Entities.Abstract;

namespace RentVilla.Domain.Entities.Concrete
{
    public class ItemAttribute : IMainEntity
    {
        public Guid Id { get; set ; }
        public AttributeType AttributeType { get; set; }
        public AttributeDesc AttributeDesc { get; set; }
        public Product Product { get; set; }
    }
}
