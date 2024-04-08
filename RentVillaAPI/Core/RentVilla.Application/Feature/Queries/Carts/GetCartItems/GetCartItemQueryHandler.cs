using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Carts.GetCartItems
{
    public class GetCartItemQueryHandler :IRequestHandler<GetCartItemQueryRequest, GetCartItemQueryResponse>
    {
        private readonly ICartService _service;
        private readonly ILogger<GetCartItemQueryHandler> _logger;

        public GetCartItemQueryHandler(ICartService service, ILogger<GetCartItemQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<GetCartItemQueryResponse> Handle(GetCartItemQueryRequest request, CancellationToken cancellationToken)
        {
            var cartItems = await _service.GetCartItemsAsync();
            if (request == null)
            {
                _logger.LogError("GetCartItemQueryRequest is null");
                throw new Exception("GetCartItemQueryRequest is null");
            }
            else
            {
                return new GetCartItemQueryResponse
                {
                    cartItemDTOs = cartItems
                };
            }
        }
    }
}
