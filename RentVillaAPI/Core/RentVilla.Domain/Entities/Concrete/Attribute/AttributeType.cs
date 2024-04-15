namespace RentVilla.Domain.Entities.Concrete.Attribute
{
    public class AttributeType
    {
        public AttributeType()
        {
            Attributes = new HashSet<Attributes>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Attributes> Attributes { get; set; }
    }
}
