using RentVilla.MVC.Models.Address;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Account
{
    public class UserDataLoginVM
    {
        [JsonPropertyName("id")]
        public string  Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("userAddress")]
        public UserAddressVM UserAddress { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("passwordConfirm")]
        public string PasswordConfirm { get; set; }
        [JsonPropertyName("profileImage")]
        public string ProfileImage { get; set; }
}
}
