using MediatR;
using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Domain.Entities.Concrete.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductAttributeWriteRepository _productAttributeWriteRepository;
        private readonly IProductAttributeReadRepository _productAttributeReadRepository;

        public DeleteProductCommandHandler(IProductReadRepository productReadRepository, IProductAttributeWriteRepository productAttributeWriteRepository, IProductWriteRepository productWriteRepository, IProductAttributeReadRepository productAttributeReadRepository)
        {
            _productReadRepository = productReadRepository;
            _productAttributeWriteRepository = productAttributeWriteRepository;
            _productWriteRepository = productWriteRepository;
            _productAttributeReadRepository = productAttributeReadRepository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productReadRepository.GetByIdAsync(request.ProductId);
                var productAttributes = new List<ProductAttribute>();
                productAttributes = _productAttributeReadRepository.AppDbContext.Where(pa => pa.Product.Id == Guid.Parse(request.ProductId)).ToList();
                
                _productAttributeWriteRepository.DeleteRange(productAttributes);
                await _productWriteRepository.DeleteAsync(request.ProductId);
                await _productWriteRepository.SaveAsync();
                return new();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
    }
}
