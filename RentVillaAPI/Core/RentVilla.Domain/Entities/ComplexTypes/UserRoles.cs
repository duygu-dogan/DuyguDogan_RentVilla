using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.ComplexTypes
{
    public enum UserRoles
    {
        SuperAdmin = 0,
        Admin = 1,
        Guest = 2,
        Landlord = 3
    }
}
