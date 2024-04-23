using MediatR;
using RentVilla.Application.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Products.GetByRegionProducts
{
    public class GetByRegionProductsQueryHandler : IRequestHandler<GetByRegionProductsQueryRequest, GetByRegionProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetByRegionProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByRegionProductsQueryResponse> Handle(GetByRegionProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productReadRepository.GetProductsByRegion(request.RegionId);
            return (new GetByRegionProductsQueryResponse
            {
                Products = products.ToList()
            });
        }
    }
}
