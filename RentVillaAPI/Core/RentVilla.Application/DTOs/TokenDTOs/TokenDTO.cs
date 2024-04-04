using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.TokenDTOs
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