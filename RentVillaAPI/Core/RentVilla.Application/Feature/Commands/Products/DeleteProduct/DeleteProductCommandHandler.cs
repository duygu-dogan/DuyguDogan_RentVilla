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
        private readonly IProductAttributeWriteRepository _productAttributeWriteRepository;
        private readonly IProductAttributeWriteRepository _productWriteRepository;

        public DeleteProductCommandHandler(IProductReadRepository productReadRepository, IProductAttributeWriteRepository productAttributeWriteRepository, IProductAttributeWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productAttributeWriteRepository = productAttributeWriteRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var productDTO = await _productReadRepository.GetJoinedProductByIdAsync(request.ProductId);
                var productAttributes = new List<ProductAttribute>();
                foreach (var productAttribute in productDTO.Attributes)
                {
                    productAttributes.Add(new ProductAttribute
                    {
                        Id = Guid.Parse(productAttribute.Id),
                        Attributes = new Attributes
                        {
                            Description = productAttribute.Attribute
                        },
                        AttributeType = new AttributeType
                        {
                            Name = productAttribute.AttributeType
                        },
                        Product = _productReadRepository.GetByIdAsync(request.ProductId).Result
                    });
                }
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
