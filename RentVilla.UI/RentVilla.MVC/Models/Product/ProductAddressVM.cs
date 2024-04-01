using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Product
{
    public class ProductAddressVM
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("countryName")]
        public string CountryName { get; set; }
        [JsonPropertyName("stateName")]
        public string StateName { get; set; }
        [JsonPropertyName("cityName")]
        public string CityName { get; set; }
        [JsonPropertyName("districtName")]
        public string DistrictName { get; set; }
    }
}
