using RentVilla.Application.Repositories.MenuRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.MenuCRepo
{
    public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
    {
        public MenuReadRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
