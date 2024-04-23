
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Product
{
    public class ProductImageVM
    {
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("productId")]
        public List<string> ProductId { get; set; }
    }
}
