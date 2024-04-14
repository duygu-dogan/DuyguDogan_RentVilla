using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Feature.Commands.ProductImages.UploadProductImages;
using RentVilla.Application.Feature.Commands.Products.CreateProduct;
using RentVilla.Application.Feature.Commands.Products.DeleteProduct;
using RentVilla.Application.Feature.Commands.Products.SoftDeleteProduct;
using RentVilla.Application.Feature.Commands.Products.UpdateProduct;
using RentVilla.Application.Feature.Queries.ProductImages.GetProductImages;
using RentVilla.Application.Feature.Queries.Products.GetAllProducts;
using RentVilla.Application.Feature.Queries.Products.GetByIdProduct;
using RentVilla.Application.Feature.Queries.Products.GetDeletedProducts;
using System.Net;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest getAllProductsQueryRequest)
        {
            GetAllProductsQueryResponse response = await _mediator.Send(getAllProductsQueryRequest);
            return Ok(response.NonDeletedProducts);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedProducts([FromQuery] GetDeletedProductsRequest request)
        {
            GetDeletedProductsResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductImages([FromQuery] GetProductImagesQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(request);
            return Ok(response.Product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProductImage([FromQuery] UploadProductImagesRequest uploadProductImagesRequest)
        {
            uploadProductImagesRequest.Files = Request.Form.Files;
            await _mediator.Send(uploadProductImagesRequest);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> SoftDelete([FromQuery] SoftDeleteProductCommandRequest commandRequest)
        {
            var response = await _mediator.Send(commandRequest);
            return Ok(response);
        }
        
    }
}
