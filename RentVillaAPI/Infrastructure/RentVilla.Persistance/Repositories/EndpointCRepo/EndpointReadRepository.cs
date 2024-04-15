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
    public class EndpointReadRepository : ReadRepository<Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
