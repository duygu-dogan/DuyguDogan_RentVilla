using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Domain.Entities.Concrete.Region;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.FileCRepo
{
    public class StateImageFileWriteRepository : WriteRepository<StateImageFile>, IStateImageFileWriteRepository
    {
        public StateImageFileWriteRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
