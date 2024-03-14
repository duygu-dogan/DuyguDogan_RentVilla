using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public List<string> ImageUrl { get; set; }
        public string MapId { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public int ShortestRentPeriod { get; set; }
        public string Properties { get; set; }
        public string Rating { get; set; }
        public ReservationStatus Status { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ProductAttribute> Attributes { get; set; }
    }
}
