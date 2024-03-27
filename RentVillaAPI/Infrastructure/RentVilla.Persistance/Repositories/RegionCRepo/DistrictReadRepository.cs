using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Domain.Entities.Concrete.Region;
using RentVilla.Persistance.Contexts;

namespace RentVilla.Persistence.Repositories.RepoCRepo
{
    public class DistrictReadRepository : ReadRepository<District>, IDistrictReadRepository
    {
        public DistrictReadRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
