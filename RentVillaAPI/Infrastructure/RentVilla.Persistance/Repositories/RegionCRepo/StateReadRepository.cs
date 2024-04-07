using RentVilla.Application.DTOs.RegionDTOs;
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
        public ICollection<StateDTO> GetAllStates()
        {
            var states = _context.States.Select(state => new StateDTO
            {
                Id = state.Id.ToString(),
                Name = state.Name,
                CountryId = state.CountryId.ToString(),
                Images = state.StateImageFiles.Select(x => x.Path).ToList()
            }).ToList();
            return states;
        }
    }
}
