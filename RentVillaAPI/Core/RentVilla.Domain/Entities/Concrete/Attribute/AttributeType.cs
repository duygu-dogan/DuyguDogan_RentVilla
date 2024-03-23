namespace RentVilla.Domain.Entities.Concrete.Attribute
{
    public class AttributeType
    {
        public AttributeType()
        {
            Attributes = new HashSet<Attributes>();
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Attributes> Attributes { get; set; }
    }
}
