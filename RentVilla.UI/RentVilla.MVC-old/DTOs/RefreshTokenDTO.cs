using System.Text.Json.Serialization;

namespace RentVilla.MVC.DTOs
{
    public class RefreshTokenDTO
    {
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
    }
}
