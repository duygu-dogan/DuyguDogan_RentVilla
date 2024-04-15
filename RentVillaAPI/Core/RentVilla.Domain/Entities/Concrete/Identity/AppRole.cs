using Microsoft.AspNetCore.Identity;
using RentVilla.Domain.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Identity
{
    public class AppRole:IdentityRole<string>
    {
        public ICollection<Endpoint> Endpoints { get; set; }

    }
}
