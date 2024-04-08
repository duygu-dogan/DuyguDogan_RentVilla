using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Cart
{
    public class GetCartItemVM
    {
        [JsonPropertyName("cartItemId")]
        public string CartItemId { get; set; }
        [JsonPropertyName("productId")]
        public string ProductId { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("adultNumber")]
        public int AdultNumber { get; set; }
        [JsonPropertyName("childrenNumber")]
        public int ChildrenNumber { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
