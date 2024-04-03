using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using RentVilla.MVC.Models.Address;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Account
{
    public class PostRegisterVM
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your first name." )]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "Please enter your user name.")]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email.")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid phone number.")]
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Please enter your gender.")]
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("userAddress")]
        public PostUserAddressVM UserAddress { get; set; }

        [DisplayName("Address Line")]
        [Required(ErrorMessage = "Please enter your address.")]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [DisplayName("Birth Date")]
        [Required(ErrorMessage = "Please enter your birth date.")]
        [DataType(DataType.DateTime)]
        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [DisplayName("Password Repeat")]
        [Required(ErrorMessage = "Please enter your password again.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match. Please check your passwords.")]
        [JsonPropertyName("passwordConfirm")]
        public string PasswordConfirm { get; set; }
        [JsonPropertyName("profileImage")]
        public string ProfileImage { get; set; } = "";

    }
}
