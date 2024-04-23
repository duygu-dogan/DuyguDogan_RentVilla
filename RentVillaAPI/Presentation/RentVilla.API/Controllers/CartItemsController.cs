using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Consts;
using RentVilla.Application.CustomAttributes;
using RentVilla.Application.Enums;
using RentVilla.Application.Feature.Commands.Carts.AddItemToCart;
using RentVilla.Application.Feature.Commands.Carts.RemoveItemFromCart;
using RentVilla.Application.Feature.Commands.Carts.UpdateItemInCart;
using RentVilla.Application.Feature.Queries.Carts.GetCartItemById;
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
        [HttpGet(Name ="GetCartItems")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.CartItems, Definition = "Gets all cart items", ActionType = ActionTypes.Reading)]
        public async Task<IActionResult> GetCartItems([FromQuery]GetCartItemQueryRequest request) 
        {
            GetCartItemQueryResponse response = await _mediator.Send(request);
            return Ok(response.cartItemDTOs);
        }
        [HttpGet("{CartItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.CartItems, Definition = "Gets cart item by id", ActionType = ActionTypes.Reading)]

        public async Task<IActionResult> GetCartItemById([FromRoute] GetCartItemByIdQueryRequest request)
        {
            GetCartItemByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response.CartItem);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.CartItems, Definition = "Adds item to cart", ActionType = ActionTypes.Writing)]
        public async Task<IActionResult> AddItemToCart(AddItemToCartCommandRequest request)
        {
            AddItemToCartCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.CartItems, Definition = "Updates item in the cart", ActionType = ActionTypes.Updating)]
        public async Task<IActionResult> UpdateItemInCart(UpdateItemInCartCommandRequest request)
        {
            UpdateItemInCartCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{CartItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.CartItems, Definition = "Deletes item by id", ActionType = ActionTypes.Deleting)]
        public async Task<IActionResult> RemoveItemFromCart ([FromRoute]RemoveItemFromCartCommandRequest request)
        {
            RemoveItemFromCartCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
