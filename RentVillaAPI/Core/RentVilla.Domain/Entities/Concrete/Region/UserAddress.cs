using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Region
{
    public class UserAddress : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public State State { get; set; }
        public City City { get; set; }
        public District District { get; set; }
    }
}
