namespace RentVilla.MVC.Models.Cart
{
    public class ReservationCartVM
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public List<GetCartItemVM> CartItems { get; set; }
        public decimal TotalCost()
        {
            return CartItems.Sum(x => x.TotalCost);
        }
    }
}
