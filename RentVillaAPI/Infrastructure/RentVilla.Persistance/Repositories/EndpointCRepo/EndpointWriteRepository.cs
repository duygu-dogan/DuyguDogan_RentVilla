using RentVilla.Application.Repositories.EndpointRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.EndpointCRepo
{
    public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
