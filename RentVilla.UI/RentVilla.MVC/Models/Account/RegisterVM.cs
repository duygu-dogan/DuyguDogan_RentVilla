using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RentVilla.MVC.Models.Account
{
    public class RegisterVM
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your first name." )]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "Please enter your user name.")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("RePassword")]
        [Required(ErrorMessage = "Please enter your password again.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match. Please check your passwords.")]
        public string RePassword { get; set; }
    }
}
