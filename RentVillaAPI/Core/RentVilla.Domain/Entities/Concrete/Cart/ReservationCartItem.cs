using RentVilla.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Cart
{
    public class ReservationCartItem: BaseEntity
    {
        public Guid ReservationCartId { get; set; }
        public Guid ProductId { get; set; }
        public ReservationCart ReservationCart { get; set; }
        public Product Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AdultNumber { get; set; }
        public int ChildrenNumber { get; set; }
        public string Note { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalCost { get; set; }
    }
}
