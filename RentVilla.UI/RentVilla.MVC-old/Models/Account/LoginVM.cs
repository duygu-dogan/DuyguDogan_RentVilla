using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email or username field is required!")]
        [DisplayName("Email or Username")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
    }
}
