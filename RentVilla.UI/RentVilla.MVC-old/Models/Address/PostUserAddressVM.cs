using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Address
{
    public class PostUserAddressVM
    {
        [JsonPropertyName("stateId")]
        public string StateId { get; set; }
        [JsonPropertyName("cityId")]
        public string CityId { get; set; }
        [JsonPropertyName("districtId")]
        public string DistrictId { get; set; }
    }
}
