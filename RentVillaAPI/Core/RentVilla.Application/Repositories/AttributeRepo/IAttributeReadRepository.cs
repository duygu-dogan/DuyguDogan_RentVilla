using RentVilla.Application.Repositories;
using RentVilla.Application.ViewModels.Attribute;
using RentVilla.Domain.Entities.Concrete.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Repositories.AttributeRepo
{
    public interface IAttributeReadRepository : IReadRepository<Attributes>
    {
        public List<AttributeReadVM> GetAttributes();
        public Task<Attributes> GetByIdWithTypeAsync(string id);
        public List<AttributeReadVM> GetAttributesByTypeId(string typeId);
    }
}
