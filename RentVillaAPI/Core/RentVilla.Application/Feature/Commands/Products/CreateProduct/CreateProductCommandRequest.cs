using MediatR;
using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Products.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public string MapId { get; set; }
        public string Address { get; set; }
        public ProductAddressDTO ProductAddress { get; set; }
        public int ShortestRentPeriod { get; set; }
        public string Properties { get; set; }
        public ICollection<string> AttributeIds { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
