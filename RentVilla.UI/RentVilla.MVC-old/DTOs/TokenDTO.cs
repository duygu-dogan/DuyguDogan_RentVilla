using System.Text.Json.Serialization;

namespace RentVilla.MVC.DTOs
{
    public class TokenDTO
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; set; }
        [JsonPropertyName("RefreshToken")]
        public string? RefreshToken { get; set; }
        [JsonPropertyName("RefreshTokenEndDate")]
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
