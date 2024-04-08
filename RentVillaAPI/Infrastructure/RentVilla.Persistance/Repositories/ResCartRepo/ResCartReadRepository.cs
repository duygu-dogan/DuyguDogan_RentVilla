using RentVilla.Application.Repositories.ReservationCartRepo;
using RentVilla.Domain.Entities.Concrete.Cart;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.ResCartRepo
{
    public class ResCartReadRepository : ReadRepository<ReservationCart>, IResCartReadRepository
    {
        public ResCartReadRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
