using RentVilla.Application.Repositories;
using RentVilla.Domain.Entities.Concrete.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Repositories.AttributeRepo
{
    public interface IAttributeTypeWriteRepository : IWriteRepository<AttributeType>
    {
        public bool SoftDelete (string id);
    }
}
