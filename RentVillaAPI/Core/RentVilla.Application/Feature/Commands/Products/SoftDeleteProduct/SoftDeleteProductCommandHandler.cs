using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Products.SoftDeleteProduct
{
    public class SoftDeleteProductCommandHandler : IRequestHandler<SoftDeleteProductCommandRequest, SoftDeleteProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        readonly ILogger<SoftDeleteProductCommandHandler> _logger;

        public SoftDeleteProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ILogger<SoftDeleteProductCommandHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _logger = logger;
        }

        public async Task<SoftDeleteProductCommandResponse> Handle(SoftDeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productReadRepository.GetByIdAsync(request.ProductId);
                product.IsDeleted = !product.IsDeleted;
                product.IsActive = !product.IsActive;
                await _productWriteRepository.SaveAsync();
                _logger.LogInformation($"Product with id {product.Id} has been sent to trash bin.");
                return new();
            }
            catch (Exception)
            {
                _logger.LogError("Product could not be send to trash bin.");
                throw;
            }
        }
    }
}
