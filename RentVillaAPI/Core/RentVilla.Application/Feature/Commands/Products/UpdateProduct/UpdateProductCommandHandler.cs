using MediatR;
using RentVilla.Application.DTOs;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
