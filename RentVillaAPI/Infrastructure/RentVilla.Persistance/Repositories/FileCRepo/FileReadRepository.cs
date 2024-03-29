using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = RentVilla.Domain.Entities.Concrete.File;

namespace RentVilla.Persistence.Repositories.FileCRepo
{
    public class FileReadRepository : ReadRepository<File>, IFileReadRepository
    {
        public FileReadRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
