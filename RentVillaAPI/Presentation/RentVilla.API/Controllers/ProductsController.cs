using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Abstraction.Storage;
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
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
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
                ImageUrl = model.ImageUrl,
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
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateVM model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Deposit = model.Deposit;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
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
            var product = await _productReadRepository.GetJoinedProductByIdAsync(id);
            _productAttributeWriteRepository.DeleteRange(product.SelectMany(x => x.Attributes).ToList());
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
