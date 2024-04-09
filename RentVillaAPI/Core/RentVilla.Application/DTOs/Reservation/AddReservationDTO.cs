using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Domain.Entities.Concrete.Cart;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.Reservation
{
    public class AddReservationDTO
    {
        public string ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AdultNumber { get; set; }
        public int ChildrenNumber { get; set; }
        public string Note { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalCost { get; set; }
    }
}
