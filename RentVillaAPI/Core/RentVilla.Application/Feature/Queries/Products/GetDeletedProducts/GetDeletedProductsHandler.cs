using MediatR;
using RentVilla.Application.DTOs;
using RentVilla.Application.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Products.GetDeletedProducts
{
    public class GetDeletedProductsHandler : IRequestHandler<GetDeletedProductsRequest, GetDeletedProductsResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetDeletedProductsHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetDeletedProductsResponse> Handle(GetDeletedProductsRequest request, CancellationToken cancellationToken)
        {
            List<ProductDTO> products = _productReadRepository.GetAllProducts().ToList();
            List<ProductDTO> deletedProducts = products.Where(x => x.IsDeleted == true).ToList();
            GetDeletedProductsResponse response = new GetDeletedProductsResponse() { DeletedProducts = deletedProducts};
            return response;
        }
    }
}
