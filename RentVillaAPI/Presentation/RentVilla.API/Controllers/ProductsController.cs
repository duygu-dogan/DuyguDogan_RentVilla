using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Consts;
using RentVilla.Application.CustomAttributes;
using RentVilla.Application.Enums;
using RentVilla.Application.Feature.Commands.ProductImages.UploadProductImages;
using RentVilla.Application.Feature.Commands.Products.CreateProduct;
using RentVilla.Application.Feature.Commands.Products.DeleteProduct;
using RentVilla.Application.Feature.Commands.Products.SoftDeleteProduct;
using RentVilla.Application.Feature.Commands.Products.UpdateProduct;
using RentVilla.Application.Feature.Queries.ProductImages.GetProductImages;
using RentVilla.Application.Feature.Queries.Products.GetAllProducts;
using RentVilla.Application.Feature.Queries.Products.GetByIdProduct;
using RentVilla.Application.Feature.Queries.Products.GetByRegionProducts;
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
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Products, Definition = "Gets deleted products", ActionType = ActionTypes.Reading)]

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

        [HttpGet]
        public async Task<IActionResult> GetByRegion([FromQuery] GetByRegionProductsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response.Products);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Products, Definition = "Adds product", ActionType = ActionTypes.Writing)]
        public async Task<IActionResult> Add(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Products, Definition = "Uploads product image", ActionType = ActionTypes.Writing)]
        public async Task<IActionResult> UploadProductImage([FromQuery] UploadProductImagesRequest uploadProductImagesRequest)
        {
            uploadProductImagesRequest.Files = Request.Form.Files;
            await _mediator.Send(uploadProductImagesRequest);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Products, Definition = "Updates product by id", ActionType = ActionTypes.Updating)]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Products, Definition = "Deletes product by id", ActionType = ActionTypes.Deleting)]
        public async Task<IActionResult> Delete([FromQuery] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Products, Definition = "Changes product isDeleted status", ActionType = ActionTypes.Updating)]
        public async Task<IActionResult> SoftDelete([FromQuery] SoftDeleteProductCommandRequest commandRequest)
        {
            var response = await _mediator.Send(commandRequest);
            return Ok(response);
        }
        
    }
}
