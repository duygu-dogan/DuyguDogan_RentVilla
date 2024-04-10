using RentVilla.MVC.Models.Product;

namespace RentVilla.MVC.Models.Reservation
{
    public class AddReservationVM
    {
        public ProductVM Product { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AdultNumber { get; set; }
        public int ChildrenNumber { get; set; }
        public string Note { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalCost { get; set; }
    }
}
