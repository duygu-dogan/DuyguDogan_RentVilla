using RentVilla.MVC.Models.Product;

namespace RentVilla.MVC.Models.Reservation
{
    public class CreateReservationVM
    {
        public string AppUserId { get; set; }
        public string ProductId { get; set; }
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
        public int PaymentType { get; set; } = 0;
        public string PaymentMethod { get; set; }
    }
}
