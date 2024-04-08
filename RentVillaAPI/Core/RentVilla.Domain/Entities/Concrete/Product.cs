using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Cart;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete
{
    public class Product : BaseEntity, IMainEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public string MapId { get; set; }
        public string Address { get; set; }
        public ProductAddress ProductAddress { get; set; }
        public int ShortestRentPeriod { get; set; }
        public string Properties { get; set; }
        public string Rating { get; set; }
        public ReservationStatusType Status { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ProductAttribute> Attributes { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public ICollection<ReservationCartItem> CartItems { get; set; }
    }
}
