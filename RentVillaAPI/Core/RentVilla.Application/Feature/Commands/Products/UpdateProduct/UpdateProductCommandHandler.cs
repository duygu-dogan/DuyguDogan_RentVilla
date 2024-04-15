using MediatR;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Region;

namespace RentVilla.Application.Feature.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductAddressReadRepository _productAddressReadRepository;
        private readonly IProductAddressWriteRepository _productAddressWriteRepository;
        private readonly IProductAttributeWriteRepository _productAttributeWriteRepository;
        private readonly IProductAttributeReadRepository _productAttributeReadRepository;
        private readonly IAttributeReadRepository _attributeReadRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IProductAddressReadRepository productAddressReadRepository, IProductAddressWriteRepository productAddressWriteRepository, IProductAttributeWriteRepository productAttributeWriteRepository, IProductAttributeReadRepository productAttributeReadRepository, IAttributeReadRepository attributeReadRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _productAddressReadRepository = productAddressReadRepository;
            _productAddressWriteRepository = productAddressWriteRepository;
            _productAttributeWriteRepository = productAttributeWriteRepository;
            _productAttributeReadRepository = productAttributeReadRepository;
            _attributeReadRepository = attributeReadRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Product product = await _productReadRepository.GetByIdAsync(request.Product.Id);
                ProductAddress productAddress = new ProductAddress();
                productAddress = _productAddressReadRepository.AppDbContext.Where(pa => pa.ProductId == product.Id).FirstOrDefault();
                productAddress.StateId = request.Product.ProductAddress.StateId;
                productAddress.CityId = request.Product.ProductAddress.CityId;
                productAddress.DistrictId = request.Product.ProductAddress.DistrictId;
                await _productAddressWriteRepository.SaveAsync();

                List<ProductAttribute> productAttributes = new();
                foreach (var att in request.Product.Attributes)
                {
                    Attributes attribute = _attributeReadRepository.AppDbContext.Include(a => a.AttributeType).Where(a => a.Id == att.Id).FirstOrDefault();
                    ProductAttribute newAttribute = new ProductAttribute()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Attributes = attribute,
                        Product = product,
                        AttributeType = attribute.AttributeType,
                    };
                    productAttributes.Add(newAttribute);
                }
                await _productAttributeWriteRepository.AddRangeAsync(productAttributes);
                await _productAttributeWriteRepository.SaveAsync();

                UpdateProductDTO newProduct = request.Product;
                product.Name = newProduct.Name;
                product.Price = newProduct.Price;
                product.Deposit = newProduct.Deposit;
                product.Description = newProduct.Description;
                product.Address = newProduct.Address;
                product.MapId = newProduct.MapId;
                product.Properties = newProduct.Properties;
                product.ShortestRentPeriod = newProduct.ShortestRentPeriod;
                product.ProductAddress = productAddress;
                product.Attributes = productAttributes;

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
