using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Domain.Entities.Concrete.Cart;

namespace RentVilla.Domain.Entities.Concrete
{
    public class Reservation : BaseEntity
    {
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AdultNumber { get; set; }
        public int ChildrenNumber { get; set; }
        public string Note { get; set; } = null;
        public double ProductPrice { get; set; }
        public double TotalCost { get; set; }
        public string ConversationId { get; set; }
        public string PaymentId { get; set; }
        public PaymentType PaymentType { get; set; }
        public string PaymentMethod { get; set; }
        public ReservationStatusType Status { get; set; }

    }
}
