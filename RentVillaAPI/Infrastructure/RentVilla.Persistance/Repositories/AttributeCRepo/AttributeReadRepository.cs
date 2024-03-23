using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.ViewModels.Attribute;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.AttributeCRepo
{
    public class AttributeReadRepository : ReadRepository<Attributes>, IAttributeReadRepository
    {
        public AttributeReadRepository(RentVillaDbContext context) : base(context)
        {
        }
        private RentVillaDbContext RentVillaDbContext
        {
            get { return _context as RentVillaDbContext; }
        }
        public List<AttributeReadVM> GetAttributes()
        {
            var attributeType = _context.AttributeTypes.ToList();
            var attributes = _context.Attributes.ToList();

            List<AttributeReadVM> models = new();
            foreach (var item in attributes)
            {
                models.Add(new AttributeReadVM
                {
                    Name = item.AttributeType.Name,
                    Description = item.Description,
                    IsActive = item.IsActive
                });
            }
            return models;
        }
    }
}
