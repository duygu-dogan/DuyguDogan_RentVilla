using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.ViewModels.Attribute;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Persistance.Contexts;

namespace RentVilla.Persistence.Repositories.AttributeCRepo
{
    public class AttributeWriteRepository : WriteRepository<Attributes>, IAttributeWriteRepository
    {
        public AttributeWriteRepository(RentVillaDbContext context) : base(context)
        {
        }
        private RentVillaDbContext RentVillaDbContext
        {
            get { return _context as RentVillaDbContext; }
        }
        
    }
}
