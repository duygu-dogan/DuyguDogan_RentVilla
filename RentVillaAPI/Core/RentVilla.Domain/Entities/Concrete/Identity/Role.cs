using Microsoft.AspNetCore.Identity;
using RentVilla.Domain.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Identity
{
    public class Role:IdentityRole
    {
        public UserRoleType UserRoles { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
