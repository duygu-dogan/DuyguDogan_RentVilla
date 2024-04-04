using MediatR;
using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Domain.Entities.Concrete;

namespace RentVilla.Application.Feature.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await _productReadRepository.GetByIdAsync(request.Product.Id);
            ProductDTO newProduct = request.Product;
            try
            {
                product.Name = newProduct.Name;
                product.Price = newProduct.Price;
                product.Deposit = newProduct.Deposit;
                product.Description = newProduct.Description;
                product.Address = newProduct.Address;
                product.MapId = newProduct.MapId;
                product.Properties = newProduct.Properties;
                product.ShortestRentPeriod = newProduct.ShortestRentPeriod;
                product.IsDeleted = newProduct.IsDeleted;
                product.IsActive = newProduct.IsActive;
                await _productWriteRepository.SaveAsync();
                return new();

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
            
        }
    }
}
