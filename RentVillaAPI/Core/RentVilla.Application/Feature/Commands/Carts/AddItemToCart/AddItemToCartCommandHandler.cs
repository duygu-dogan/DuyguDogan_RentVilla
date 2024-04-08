using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Carts.AddItemToCart
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommandRequest, AddItemToCartCommandResponse>
    {
        private readonly ICartService _service;
        private readonly ILogger<AddItemToCartCommandHandler> _logger;

        public AddItemToCartCommandHandler(ICartService service, ILogger<AddItemToCartCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<AddItemToCartCommandResponse> Handle(AddItemToCartCommandRequest request, CancellationToken cancellationToken)
        {
            await _service.AddItemToCartAsync(request.cartItemDTO);
            if (request.cartItemDTO == null)
            {
                _logger.LogError("Cart item is null");
                throw new Exception("Cart item is null");
            }
            else
                return new AddItemToCartCommandResponse();
        }
    }
}
