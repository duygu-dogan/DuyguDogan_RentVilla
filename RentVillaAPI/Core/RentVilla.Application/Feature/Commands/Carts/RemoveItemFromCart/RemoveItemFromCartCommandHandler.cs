using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Carts.RemoveItemFromCart
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommandRequest, RemoveItemFromCartCommandResponse>
        
    {
        private readonly ICartService _service;
        private readonly ILogger<RemoveItemFromCartCommandHandler> _logger;

        public RemoveItemFromCartCommandHandler(ICartService service, ILogger<RemoveItemFromCartCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<RemoveItemFromCartCommandResponse> Handle(RemoveItemFromCartCommandRequest request, CancellationToken cancellationToken)
        {
           await _service.RemoveItemFromCartAsync(request.CartItemId);
            if (request.CartItemId == null)
            {
                _logger.LogError("CartItemId is null");
                throw new Exception("CartItemId is null");
            }
            else
            {
                return new RemoveItemFromCartCommandResponse();
            }
        }
    }
}
