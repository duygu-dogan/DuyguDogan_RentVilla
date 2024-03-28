using Microsoft.EntityFrameworkCore;
using RentVilla.Application.DTOs;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.ProductCRepo
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(RentVillaDbContext context) : base(context)
        {

        }

        public ICollection<ProductDTO> GetAllProducts()
        {
            var products = _context.Products.ToList();
            var productAttibutes = _context.ProductAttributes
        .Include(pat => pat.Attributes)
        .Include(pat => pat.AttributeType)
        .ToList();
            var productAddresses = _context.ProductAddresses
                .Include(pa => pa.Country)
                .Include(pa => pa.State)
                .Include(pa => pa.City)
                .Include(pa => pa.District)
                .ToList();
            var productDTOs = new List<ProductDTO>();
            foreach(var product in products)
            {
                productDTOs.Add(new ProductDTO
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Deposit = product.Deposit,
                    ImageUrl = product.ImageUrl,
                    MapId = product.MapId,
                    Address = product.Address,
                    ShortestRentPeriod = product.ShortestRentPeriod,
                    Properties = product.Properties,
                    Rating = product.Rating,
                    Status = product.Status,
                    Reservations = product.Reservations,
                    Attributes = new List<ProductAttributeDTO>
                    {
                        new ProductAttributeDTO
                        {
                            Id = productAttibutes.FirstOrDefault().Id.ToString(),
                            Attribute = productAttibutes.FirstOrDefault().Attributes.Description,
                            AttributeType = productAttibutes.FirstOrDefault().AttributeType.Name
                        }
                    },
                    ProductAddress = new ProductAddressDTO
                    {
                        CountryName = productAddresses.FirstOrDefault().Country.Name,
                        StateName = productAddresses.FirstOrDefault().State.Name,
                        CityName = productAddresses.FirstOrDefault().City.Name,
                        DistrictName = productAddresses.FirstOrDefault().District.Name
                    },
                    IsActive = product.IsActive,
                    IsDeleted = product.IsDeleted
                });
            }
            return productDTOs;
        }

        public Task<IEnumerable<Product>> GetJoinedProductByIdAsync(string id)
        {
           var product = _context.Products.Where(x => x.Id == Guid.Parse(id)).Include(x => x.ProductAddress).Include(x => x.Attributes).ToList();
            return Task.FromResult(product.AsEnumerable());
        }

        public Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByRegion(string region)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByShortestRentTime()
        {
            throw new NotImplementedException();
        }
    }
}
