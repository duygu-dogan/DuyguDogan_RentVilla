using Microsoft.EntityFrameworkCore;
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
        public List<AttributeReadVM> GetAttributesByTypeId(string typeId)
        {
            AttributeType attributeType = _context.AttributeTypes.FirstOrDefault(x => x.Id.ToString() == typeId);
            if (attributeType == null)
            {
                throw new Exception("Attribute type not found");
            }
            else
            {
                var attributes = _context.Attributes.Where(x => x.AttributeType.Id == attributeType.Id).ToList();
                List<AttributeReadVM> models = new();
                foreach (var item in attributes)
                {
                    models.Add(new AttributeReadVM
                    {
                        Id = item.Id.ToString(),
                        Name = item.AttributeType.Name,
                        Description = item.Description,
                        IsActive = item.IsActive
                    });
                }
                return models;
            }
        }

        public async Task<Attributes> GetByIdWithTypeAsync(string id)
        {
            var attribute = await _context.Attributes.Include(x => x.AttributeType).FirstOrDefaultAsync(x => x.Id.ToString() == id);
            
            return attribute;
        }
    }
}
