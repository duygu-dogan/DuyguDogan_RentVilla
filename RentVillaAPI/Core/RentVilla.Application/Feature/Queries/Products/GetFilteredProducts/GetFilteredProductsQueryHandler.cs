using MediatR;
using RentVilla.Application.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Products.GetFilteredProducts
{
    public class GetFilteredProductsQueryHandler : IRequestHandler<GetFilteredProductsQueryRequest, GetFilteredProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetFilteredProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetFilteredProductsQueryResponse> Handle(GetFilteredProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var productDTOS = await _productReadRepository.GetProductsByFilter(request.Filters);
            return new GetFilteredProductsQueryResponse { FilteredProducts = productDTOS.ToList() };
        }
    }
}
