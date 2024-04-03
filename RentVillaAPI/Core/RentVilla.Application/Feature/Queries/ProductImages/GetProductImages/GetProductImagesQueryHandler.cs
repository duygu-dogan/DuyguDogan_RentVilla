using MediatR;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.DTOs.ProductDTOs;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Domain.Entities.Concrete;

namespace RentVilla.Application.Feature.Queries.ProductImages.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, GetProductImagesQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductImagesQueryHandler(IProductReadRepository readRepository)
        {
            _productReadRepository = readRepository;
        }

        public async Task<GetProductImagesQueryResponse> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Product? product = _productReadRepository.AppDbContext.Include(x => x.ProductImageFiles).FirstOrDefault(x => x.Id == Guid.Parse(request.ProductId));
                GetProductImagesQueryResponse response = new();
                if (product != null)
                {
                    response.ProductImages = product.ProductImageFiles.Select(p => new ProductImageDTO { FileName = p.FileName, Path = p.Path }).ToList();
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
            
        }
    }
}
