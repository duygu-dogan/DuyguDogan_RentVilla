using Microsoft.AspNetCore.Identity;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Identity
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public UserAddress UserAddress { get; set; }
        public string ProfileImage { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
