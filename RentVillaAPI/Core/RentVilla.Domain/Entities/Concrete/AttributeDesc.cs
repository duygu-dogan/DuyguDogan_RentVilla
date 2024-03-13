using RentVilla.Domain.Entities.Abstract;

namespace RentVilla.Domain.Entities.Concrete
{
    public class AttributeDesc : IMainEntity
    {
        public Guid Id { get; set; }
        public AttributeType AttributeType { get; set; }
        public string Description { get; set; }
    }
}
