using RentVilla.MVC.Models.Product;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Cart
{
    public class AddCartItemVM
    {
        [JsonPropertyName("ProductId")]
        public string ProductId { get; set; }
        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("AdultNumber")]
        public int AdultNumber { get; set; }
        [JsonPropertyName("ChildrenNumber")]
        public int ChildrenNumber { get; set; }
        [JsonPropertyName("Note")]
        public string Note { get; set; } = null;
        [JsonPropertyName("Price")]
        public decimal ProductPrice { get; set; }
        [JsonPropertyName("TotalCost")]
        public decimal TotalCost { get; set; }
    }
}

