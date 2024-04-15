using Microsoft.EntityFrameworkCore;
using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Persistance.Contexts;

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
                var products = _context.Products.Include(x => x.Attributes).ThenInclude(pa => pa.Attributes).ThenInclude(a => a.AttributeType).Include(x => x.ProductImageFiles).Include(x => x.ProductAddress).ThenInclude(pa => pa.District).ThenInclude(d => d.City).ThenInclude(c => c.State).ThenInclude(s => s.Country).ToList();
                List<ProductDTO> productDTOs = new();
                foreach (var product in products)
                {
                    ProductDTO productDTO = new()
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
                        IsActive = product.IsActive,
                        IsDeleted = product.IsDeleted,
                        ProductAddress = new ProductAddressDTO
                        {
                            CountryName = product.ProductAddress.Country.Name,
                            CountryId = product.ProductAddress.CountryId.ToString(),
                            StateName = product.ProductAddress.State.Name,
                            StateId = product.ProductAddress.StateId.ToString(),
                            CityName = product.ProductAddress.City.Name,
                            CityId = product.ProductAddress.CityId.ToString(),
                            DistrictName = product.ProductAddress.District.Name,
                            DistrictId = product.ProductAddress.DistrictId.ToString()
                        },
                        Attributes = product.Attributes.Select(x => new ProductAttributeDTO
                        {
                            Id = x.Id.ToString(),
                            Attribute = x.Attributes.Description,
                            AttributeType = x.AttributeType.Name
                        }).ToList(),
                        ProductImages = product.ProductImageFiles.Select(x => new ProductImageDTO
                        {
                            FileName = x.FileName,
                            Path = x.Path
                        }).ToList()
                    };
                    productDTOs.Add(productDTO);
                };
                return productDTOs;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ProductDTO> GetJoinedProductByIdAsync(string id)
        {
            var product = await _context.Products.FindAsync(id);
            var productAddress = _context.ProductAddresses.Where(x => x.ProductId == product.Id).Include(x => x.City).Include(x => x.State).Include(x=> x.District).Include(x => x.Country).FirstOrDefault();
            var productAttributes = _context.ProductAttributes.Where(x => x.Product.Id == product.Id).Include(x => x.Attributes).Include(x => x.AttributeType).ToList();
            var productImages = _context.ProductImageFiles.Where(x => x.Product.Any(p => p.Id == product.Id)).Include(x => x.Product).ToList();
            List<ProductImageDTO> productImageDTOs = new();
            foreach (var item in productImages)
            {
                ProductImageDTO productImageDTO = new()
                {
                    FileName = item.FileName,
                    Path = item.Path
                };
                productImageDTOs.Add(productImageDTO);
            }
           
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
                MapId = product.MapId,
                Address = product.Address,
                ShortestRentPeriod = product.ShortestRentPeriod,
                Properties = product.Properties,
                Rating = product.Rating,
                Reservation = new Application.DTOs.ReservationDTOs.CreateReservationDTO(),
                IsActive = product.IsActive,
                IsDeleted = product.IsDeleted,
                ProductAddress = new ProductAddressDTO
                {
                    CountryName = productAddress.Country.Name,
                    CountryId = productAddress.CountryId.ToString(),
                    StateName = productAddress.State.Name,
                    StateId = productAddress.StateId.ToString(),
                    CityName = productAddress.City.Name,
                    CityId = productAddress.CityId.ToString(),
                    DistrictName = productAddress.District.Name,
                    DistrictId = productAddress.DistrictId.ToString()
                    
                },
                Attributes = productAttributesDTOs,
                ProductImages = productImageDTOs
            };
            return productDTO;
        }

        public Task<IEnumerable<Product>> GetProductsByPriceRange(double minPrice, double maxPrice)
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
