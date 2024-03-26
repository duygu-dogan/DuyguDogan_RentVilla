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
    public class AttributeTypeWriteRepository : WriteRepository<AttributeType>, IAttributeTypeWriteRepository
    {
        private readonly RentVillaDbContext _context;
        public AttributeTypeWriteRepository(RentVillaDbContext context) : base(context)
        {
            _context = context;
        }
        
        public bool SoftDelete(string id)
        {
            AttributeType attributeType = _context.AttributeTypes.Find(id);
            if (attributeType == null)
            {
                return false;
            }
            else
            {
                attributeType.IsDeleted = true;
                return true;
            }
        }
    }
}
