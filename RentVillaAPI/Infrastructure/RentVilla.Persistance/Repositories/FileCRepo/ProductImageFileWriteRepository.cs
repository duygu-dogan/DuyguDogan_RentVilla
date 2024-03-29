using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Persistance.Contexts;

namespace RentVilla.Persistence.Repositories.FileCRepo
{
    public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
