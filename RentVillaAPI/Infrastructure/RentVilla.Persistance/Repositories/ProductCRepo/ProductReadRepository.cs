using Microsoft.EntityFrameworkCore;
using RentVilla.Application.DTOs;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Region;
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
            try
            {
                var productDTOs = _context.Products.Select(product => new ProductDTO
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Deposit = product.Deposit,
                    MapId = product.MapId,
                    Address = product.Address,
                    ShortestRentPeriod = product.ShortestRentPeriod,
                    Properties = product.Properties,
                    Rating = product.Rating,
                    Status = product.Status,
                    Reservations = product.Reservations,
                    IsActive = product.IsActive,
                    IsDeleted = product.IsDeleted,
                    Attributes = _context.ProductAttributes
                    .Where(pa => pa.Product.Id == product.Id)
                    .Select(pa => new ProductAttributeDTO
                    {
                        Id = pa.Id.ToString(),
                        Attribute = pa.Attributes.Description,
                        AttributeType = pa.AttributeType.Name
                    }).ToList(),
                    ProductImages = _context.ProductImageFiles
                    .Where(pif => pif.Product.Any(p => p.Id == product.Id))
                    .Select(image => new ProductImageDTO
                    {
                        FileName = image.FileName,
                        Path = image.Path,
                        ProductId = image.Product.Select(p => p.Id.ToString()).ToList()
                    }).ToList(),
                    ProductAddress = _context.ProductAddresses
                    .Where(pa => pa.ProductId == product.Id)
                    .Select(pa => new ProductAddressDTO
                    {
                        CountryName = pa.Country.Name,
                        StateName = pa.State.Name,
                        CityName = pa.City.Name,
                        DistrictName = pa.District.Name
                    }).FirstOrDefault()
                }).ToList();
                return productDTOs;
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
        }

        public async Task<ProductDTO> GetJoinedProductByIdAsync(string id)
        {
            var product = await _context.Products.FindAsync(Guid.Parse(id));
            var productAddress = _context.ProductAddresses.Where(x => x.ProductId == product.Id).Include(x => x.City).Include(x => x.State).Include(x=> x.District).Include(x => x.Country).FirstOrDefault();
            var productAttributes = _context.ProductAttributes.Where(x => x.Product.Id == product.Id).Include(x => x.Attributes).Include(x => x.AttributeType).ToList();
            List<ProductAttributeDTO> productAttributesDTOs = new();
            foreach (var productAttibutes in productAttributes)
            {
                productAttributesDTOs.Add(new ProductAttributeDTO
                {
                    Id = productAttibutes.Id.ToString(),
                    Attribute = productAttibutes.Attributes.Description,
                    AttributeType = productAttibutes.AttributeType.Name
                });
            }
            var productDTO = new ProductDTO
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Deposit = product.Deposit,
                //ImageUrl = product.ImageUrl,
                MapId = product.MapId,
                Address = product.Address,
                ShortestRentPeriod = product.ShortestRentPeriod,
                Properties = product.Properties,
                Rating = product.Rating,
                Status = product.Status,
                Reservations = product.Reservations,
                IsActive = product.IsActive,
                IsDeleted = product.IsDeleted,
                ProductAddress = new ProductAddressDTO
                {
                    CountryName = productAddress.Country.Name,
                    StateName = productAddress.State.Name,
                    CityName = productAddress.City.Name,
                    DistrictName = productAddress.District.Name
                },
                Attributes = productAttributesDTOs
            };
            return productDTO;
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
