using RentVilla.MVC.DTOs;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Account
{
    public class LoginResponseVM
    {
        [JsonPropertyName("token")]
        public TokenDTO? Token { get; set; }
    }
}
