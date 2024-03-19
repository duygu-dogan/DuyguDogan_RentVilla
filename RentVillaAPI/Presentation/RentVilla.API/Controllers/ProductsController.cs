using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.ViewModels.Product;
using RentVilla.Domain.Entities.Concrete;
using System.Net;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productReadRepository.GetAll(false));
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
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Deposit = model.Deposit,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Address = model.Address,
                Attributes = model.Attributes,
                MapId = model.MapId,
                ProductAddress = model.ProductAddress,
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
            await _productWriteRepository.DeleteAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
