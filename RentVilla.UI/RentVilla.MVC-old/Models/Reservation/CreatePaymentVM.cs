using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Reservation
{
    public class CreatePaymentVM
    {
        [JsonPropertyName("FirstName")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string FirstName { get; set; }
        [JsonPropertyName("LastName")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string LastName { get; set; }
        [JsonPropertyName("Address")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string Address { get; set; }
        [JsonPropertyName("City")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string City { get; set; }
        [JsonPropertyName("Email")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string Email { get; set; }
        [JsonPropertyName("PhoneNumber")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string PhoneNumber { get; set; }
        public string Note { get; set; } = null;
        [JsonPropertyName("CardName")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string CardName { get; set; }
        [JsonPropertyName("CardNumber")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string CardNumber { get; set; }
        [JsonPropertyName("ExpirationMonth")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string ExpirationMonth { get; set; }
        [JsonPropertyName("ExpirationYear")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string ExpirationYear { get; set; }
        [JsonPropertyName("Cvc")]
        [Required(ErrorMessage = "{0} field cannot be empty!")]
        public string Cvc { get; set; }
    }
}
