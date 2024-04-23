using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Region
{
    public class StateVM
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("countryId")]
        public string CountryId { get; set; }
        [JsonPropertyName("images")]
        public List<string> Images { get; set; }
    }
}
