using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Region;
using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentVilla.Application.ViewModels.Reservation;

namespace RentVilla.Application.ViewModels.Product
{
    public class ProductReadVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public List<string> ImageUrl { get; set; }
        public string MapId { get; set; }
        public string Address { get; set; }
        public ProductAddress ProductAddress { get; set; }
        public int ShortestRentPeriod { get; set; }
        public string Properties { get; set; }
        public AddReservationVM? AddReservation { get; set; }
        public ICollection<string> AttributeIDs { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
