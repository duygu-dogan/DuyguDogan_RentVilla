using RentVilla.Domain.Entities.Abstract;

namespace RentVilla.Domain.Entities.Concrete
{
    public class AttributeType: IMainEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
