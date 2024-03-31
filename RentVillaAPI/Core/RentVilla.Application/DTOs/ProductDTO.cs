using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Domain.Entities.Concrete;

namespace RentVilla.Application.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public List<ProductImageDTO> ProductImages { get; set; }
        public string MapId { get; set; }
        public string Address { get; set; }
        public ProductAddressDTO ProductAddress { get; set; }
        public int ShortestRentPeriod { get; set; }
        public string Properties { get; set; }
        public string Rating { get; set; }
        public ReservationStatusType Status { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ProductAttributeDTO> Attributes { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
