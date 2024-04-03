using Microsoft.AspNetCore.Identity;
using RentVilla.Domain.Entities.Concrete.Region;

namespace RentVilla.Domain.Entities.Concrete.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public UserAddress UserAddress { get; set; }
        public string ProfileImage { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
