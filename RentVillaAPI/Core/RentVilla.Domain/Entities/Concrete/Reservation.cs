using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.ComplexTypes;

namespace RentVilla.Domain.Entities.Concrete
{
    public class Reservation : IMainEntity
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AdultNumber { get; set; }
        public int ChildrenNumber { get; set; }
        public string Note { get; set; }
        public decimal ProductPrice { get; set; }
        public List<AddOns> Addons { get; set; }
        public decimal AddOnCost { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsPaid { get; set; }
        public string ConversationId { get; set; }
        public string PaymentId { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }

    }
}
