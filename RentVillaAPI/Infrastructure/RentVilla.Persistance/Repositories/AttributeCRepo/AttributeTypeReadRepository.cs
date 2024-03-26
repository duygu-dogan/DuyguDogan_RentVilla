using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.AttributeCRepo
{
    public class AttributeTypeReadRepository : ReadRepository<AttributeType>, IAttributeTypeReadRepository
    {
        private readonly RentVillaDbContext _context;
        public AttributeTypeReadRepository(RentVillaDbContext context) : base(context)
        {
            _context = context;
        }

        public List<AttributeType> GetDeletedAttributeTypes()
        {
            var attributeTypes = _context.AttributeTypes.Where(x => x.IsDeleted == true).ToList();
            if (attributeTypes != null)
            {
                return attributeTypes;
            }
            else
            {
                return null;
            }
        }

        public List<AttributeType> GetNonDeletedAttributeTypes()
        {
            var attributeTypes = _context.AttributeTypes.Where(x => x.IsDeleted == false).ToList();
            if (attributeTypes != null)
            {
                return attributeTypes;
            }
            else
            {
                return null;
            }
        }
    }
}
