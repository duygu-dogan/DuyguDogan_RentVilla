using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Domain.Entities.Concrete.Region;
using RentVilla.Persistance.Contexts;

namespace RentVilla.Persistence.Repositories.RegionCRepo
{
    public class StateReadRepository : ReadRepository<State>, IStateReadRepository
    {
        public StateReadRepository(RentVillaDbContext context) : base(context)
        {

        }   
    }
}
