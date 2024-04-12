using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;

namespace RentVilla.Application.Feature.Queries.Carts.GetCartItemById
{
    public class GetCartItemByIdQueryHandler: IRequestHandler<GetCartItemByIdQueryRequest, GetCartItemByIdQueryResponse>
    {
        private readonly ICartService _service;
        private readonly ILogger<GetCartItemByIdQueryHandler> _logger;

        public GetCartItemByIdQueryHandler(ICartService service, ILogger<GetCartItemByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<GetCartItemByIdQueryResponse> Handle(GetCartItemByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var cartItem = await _service.GetCartItemByIdAsync(request.CartItemId);
            if (cartItem == null)
            {
                _logger.LogError("Cart item not found");
                throw new Exception("Cart item not found");
            }
            else
            {
                return new GetCartItemByIdQueryResponse() { CartItem = cartItem };
            }
        }
    }
}
