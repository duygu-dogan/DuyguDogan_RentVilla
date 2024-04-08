using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Carts.UpdateItemInCart
{
    public class UpdateItemInCartCommandHandler : IRequestHandler<UpdateItemInCartCommandRequest, UpdateItemInCartCommandResponse>
    {
        private readonly ICartService _service;
        private readonly ILogger<UpdateItemInCartCommandHandler> _logger;

        public UpdateItemInCartCommandHandler(ICartService service, ILogger<UpdateItemInCartCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<UpdateItemInCartCommandResponse> Handle(UpdateItemInCartCommandRequest request, CancellationToken cancellationToken)
        {
            await _service.UpdateItemInCartAsync(request.cartItemDTO);
            if (request.cartItemDTO == null)
            {
                _logger.LogError("UpdateItemInCartCommandRequest is null");
                throw new Exception("UpdateItemInCartCommandRequest is null");
            }
            else
            {
                return new UpdateItemInCartCommandResponse();
            }
        }
    }
}
