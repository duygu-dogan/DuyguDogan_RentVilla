using MediatR;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Products.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = _productReadRepository.GetAllProducts();
            var nonDeletedProducts = products.Where(x => x.IsDeleted == false).Skip(request.Page * request.Size).Take(request.Size).ToList();
            var response = new GetAllProductsQueryResponse() { NonDeletedProducts = nonDeletedProducts };
            return response;
        }
    }
}
