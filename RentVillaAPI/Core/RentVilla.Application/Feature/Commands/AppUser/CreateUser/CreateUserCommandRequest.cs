using MediatR;
using RentVilla.Application.DTOs.UserDTOs;

namespace RentVilla.Application.Feature.Commands.AppUser.CreateUser
{
    public class CreateUserCommandRequest: IRequest<CreateUserCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public UserAddressDTO UserAddress { get; set; }
        public string ProfileImage { get; set; }
    }
}
