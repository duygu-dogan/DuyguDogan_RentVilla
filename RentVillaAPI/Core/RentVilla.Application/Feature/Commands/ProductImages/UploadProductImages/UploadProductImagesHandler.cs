using MediatR;
using RentVilla.Application.Abstraction.Storage;
using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Domain.Entities.Concrete;

namespace RentVilla.Application.Feature.Commands.ProductImages.UploadProductImages
{
    public class UploadProductImagesHandler : IRequestHandler<UploadProductImagesRequest, UploadProductImagesResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IStorageService _storageService;

        public UploadProductImagesHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
        }

        public async Task<UploadProductImagesResponse> Handle(UploadProductImagesRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string containerName)> result = await _storageService.UploadAsync("product-images", request.Files);
            Product product = await _productReadRepository.GetByIdAsync(request.ProductId, true);
            try
            {
                await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
                {
                    FileName = r.fileName,
                    Path = r.containerName,
                    Storage = _storageService.StorageName,
                    Product = new List<Product>() { product }
                }).ToList());
                await _productImageFileWriteRepository.SaveAsync();
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
