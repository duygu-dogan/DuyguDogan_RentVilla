using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Cart
{
    public class ReservationCart: BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Reservation Reservation { get; set; }
        public ICollection<ReservationCartItem> CartItems { get; set; }

    }
}
