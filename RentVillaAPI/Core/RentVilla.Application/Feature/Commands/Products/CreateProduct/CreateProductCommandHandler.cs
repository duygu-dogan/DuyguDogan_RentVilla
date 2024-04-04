using MediatR;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Region;

namespace RentVilla.Application.Feature.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductAddressReadRepository _productAddressReadRepository;
        private readonly IAttributeReadRepository _attributeReadRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IAttributeReadRepository attributeReadRepository, IProductAddressReadRepository productAddressReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productAddressReadRepository = productAddressReadRepository;
            _attributeReadRepository = attributeReadRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var attributes  = _attributeReadRepository.AppDbContext.Where(a => request.AttributeIds.Contains(a.Id.ToString())).Include(a => a.AttributeType).ToList();
            List<ProductAttribute> productAttributes = new();
            foreach (var attribute in attributes)
            {
                productAttributes.Add(new()
                {
                    Attributes = attribute,
                    AttributeType = attribute.AttributeType
                });
            }
            ProductAddress productAddress = new()
            {
                CountryId = Guid.Parse("3240f95b-7adc-4257-8dd3-c91de2b14217"),
                StateId = Guid.Parse(request.ProductAddress.StateId),
                CityId = Guid.Parse(request.ProductAddress.CityId),
                DistrictId = Guid.Parse(request.ProductAddress.DistrictId)
            };
            
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Deposit = request.Deposit,
                Description = request.Description,
                Address = request.Address,
                MapId = request.MapId,
                Properties = request.Properties,
                ShortestRentPeriod = request.ShortestRentPeriod,
                Attributes = productAttributes,
                ProductAddress = productAddress
            });
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
