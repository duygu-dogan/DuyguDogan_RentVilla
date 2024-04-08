using RentVilla.Application.Repositories.ReservationCartItemRepo;
using RentVilla.Domain.Entities.Concrete.Cart;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.ResCartItemRepo
{
    public class ResCartItemWriteRepository : WriteRepository<ReservationCartItem>, IResCartItemWriteRepository
    {
        public ResCartItemWriteRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
