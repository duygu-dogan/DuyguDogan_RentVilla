using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Feature.Commands.Carts.AddItemToCart;
using RentVilla.Application.Feature.Commands.Carts.RemoveItemFromCart;
using RentVilla.Application.Feature.Commands.Carts.UpdateItemInCart;
using RentVilla.Application.Feature.Queries.Carts.GetCartItems;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class CartItemsController : ControllerBase
        {
        private readonly IMediator _mediator;

        public CartItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetCartItems([FromQuery]GetCartItemQueryRequest request) 
        {
            GetCartItemQueryResponse response = await _mediator.Send(request);
            return Ok(response.cartItemDTOs);
        }
        [HttpPost]
        public async Task<IActionResult> AddItemToCart(AddItemToCartCommandRequest request)
        {
            AddItemToCartCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateItemInCart(UpdateItemInCartCommandRequest request)
        {
            UpdateItemInCartCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{CartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart ([FromRoute]RemoveItemFromCartCommandRequest request)
        {
            RemoveItemFromCartCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
