using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Abstraction.Storage;
using RentVilla.Application.DTOs;
using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Application.RequestParameters;
using RentVilla.Application.ViewModels.Product;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Region;
using System.Net;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IAttributeReadRepository _attributeReadRepository;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly IStateReadRepository _stateReadRepository;
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly IProductAttributeWriteRepository _productAttributeWriteRepository;
        private readonly IProductAttributeReadRepository _productAttributeReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IStorageService _storageService;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IAttributeReadRepository attributeReadRepository, ICityReadRepository cityReadRepository, IStateReadRepository stateReadRepository, IDistrictReadRepository districtReadRepository, ICountryReadRepository countryReadRepository, IProductAttributeWriteRepository productAttributeWriteRepository, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService, IProductAttributeReadRepository productAttributeReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _attributeReadRepository = attributeReadRepository;
            _cityReadRepository = cityReadRepository;
            _stateReadRepository = stateReadRepository;
            _districtReadRepository = districtReadRepository;
            _countryReadRepository = countryReadRepository;
            _productAttributeWriteRepository = productAttributeWriteRepository;
            _productReadRepository = productReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
            _productAttributeReadRepository = productAttributeReadRepository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]Pagination pagination)
        {
            var products = _productReadRepository.GetAllProducts();
            var nonDeletedProducts = products.Where(x => x.IsDeleted == false).Skip(pagination.Page * pagination.Size).Take(pagination.Size);
            return Ok(nonDeletedProducts);
        }

        [HttpGet]
        public IActionResult GetDeletedProducts()
        {
            var products = _productReadRepository.GetAllProducts();
            var deletedProducts = products.Where(x => x.IsDeleted == true);
            return Ok(deletedProducts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
           Product? product = _productReadRepository.AppDbContext.Include(x => x.ProductImageFiles).FirstOrDefault(x => x.Id == Guid.Parse(id)); 
            if (product != null)
            {
                return Ok(product.ProductImageFiles.Select(p => new { p.FileName, p.Path }));
            }
            return NotFound();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productReadRepository.GetJoinedProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateVM model)
        {
            List<ProductAttribute> productAttributes = new List<ProductAttribute>();
            foreach (var id in model.AttributeIDs)
            {
                Attributes attribute = await _attributeReadRepository.GetByIdWithTypeAsync(id);

                ProductAttribute productAttribute = new()
                {
                    Attributes = attribute,
                    AttributeType = attribute.AttributeType
                };

                productAttributes.Add(productAttribute);
            }
            var productAddress = new ProductAddress
            {
                Country = await _countryReadRepository.GetByIdAsync("3240f95b-7adc-4257-8dd3-c91de2b14217"),
                State = await _stateReadRepository.GetByIdAsync(model.ProductAddress.StateId.ToString()),
                City = await _cityReadRepository.GetByIdAsync(model.ProductAddress.CityId.ToString()),
                District = await _districtReadRepository.GetByIdAsync(model.ProductAddress.DistrictId.ToString())
            };
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Deposit = model.Deposit,
                Description = model.Description,
                Address = model.Address,
                Attributes = productAttributes,
                MapId = model.MapId,
                ProductAddress = productAddress,
                Properties = model.Properties,
                ShortestRentPeriod = model.ShortestRentPeriod
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UploadProductImage(string id)
        {
            List<(string fileName, string containerName)> result = await _storageService.UploadAsync("product-images", Request.Form.Files);
            Product product = await _productReadRepository.GetByIdAsync(id, true);
            try
            {
                await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
                {
                    FileName = r.fileName,
                    Path = r.containerName,
                    Storage = _storageService.StorageName,
                    Product = new List<Product> () { product }
                }).ToList());
                await _productImageFileWriteRepository.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateVM model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Deposit = model.Deposit;
            product.Description = model.Description;
            product.Address = model.Address;
            product.Attributes = model.Attributes;
            product.MapId = model.MapId;
            product.ProductAddress = model.ProductAddress;
            product.Properties = model.Properties;
            product.ShortestRentPeriod = model.ShortestRentPeriod;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var productDTO = await _productReadRepository.GetJoinedProductByIdAsync(id);
            var productAttributes = new List<ProductAttribute>();
            foreach (var productAttribute in productDTO.Attributes)
            {
                productAttributes.Add(new ProductAttribute
                {
                    Id = Guid.Parse(productAttribute.Id),
                    Attributes = new Attributes
                    {
                        Description = productAttribute.Attribute
                    },
                    AttributeType = new AttributeType
                    {
                        Name = productAttribute.AttributeType
                    },
                    Product= _productReadRepository.GetByIdAsync(id).Result
                });
            }
            _productAttributeWriteRepository.DeleteRange(productAttributes);
            await _productWriteRepository.DeleteAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            product.IsDeleted = !product.IsDeleted;
            product.IsActive = !product.IsActive;
            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var datas = await _storageService.UploadAsync("product-images", Request.Form.Files);
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage= _storageService.StorageName
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
