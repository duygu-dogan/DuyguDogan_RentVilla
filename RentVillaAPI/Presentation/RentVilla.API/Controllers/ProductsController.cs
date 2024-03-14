using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Domain.Entities.Concrete;

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
        public async Task AddProduct()
        { 
            await _productWriteRepository.AddAsync(new Product
            {
                Name = "Villa",
                Description = "Villa",
                Price = 1000
                //ImageUrl= {"villa1.png"}
            });
            await _productWriteRepository.SaveAsync();
            
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products =  _productReadRepository.GetAll();
            return Ok(products);
        }

    }
}
