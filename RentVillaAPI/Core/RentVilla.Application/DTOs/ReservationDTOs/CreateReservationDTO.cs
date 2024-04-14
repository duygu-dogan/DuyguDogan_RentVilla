using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Domain.Entities.Concrete.Cart;

namespace RentVilla.Application.DTOs.ReservationDTOs
{
    public class CreateReservationDTO
    {
        public string AppUserId { get; set; }
        public string ProductId  { get; set; }
        public string ProductName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AdultNumber { get; set; }
        public int ChildrenNumber { get; set; }
        public string Note { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsPaid { get; set; }
        public string ConversationId { get; set; }
        public string PaymentId { get; set; }
        public int PaymentType { get; set; }
        public string PaymentMethod { get; set; }
        public CreatePaymentDTO PaymentData { get; set; }
    }
}
