
using RentVilla.MVC.Models.Cart;
using RentVilla.MVC.Models.Reservation;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Product
{
    public class ProductVM
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("deposit")]
        public decimal Deposit { get; set; }
        [JsonPropertyName("productImages")]
        public List<ProductImageVM>? ProductImages { get; set; }
        [JsonPropertyName("mapId")]
        public string? MapId { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("productAddress")]
        public ProductAddressVM? ProductAddress { get; set; }
        [JsonPropertyName("shortestRentPeriod")]
        public int ShortestRentPeriod { get; set; }
        [JsonPropertyName("properties")]
        public string? Properties { get; set; }
        [JsonPropertyName("rating")]
        public string? Rating { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("attributes")]
        public ICollection<ProductAttributeVM>? Attributes { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }
        [JsonPropertyName("reservation")]
        public AddCartItemVM? AddCartItem { get; set; }
        public ProductFilterVM FilterModel { get; set; }
    }
}
