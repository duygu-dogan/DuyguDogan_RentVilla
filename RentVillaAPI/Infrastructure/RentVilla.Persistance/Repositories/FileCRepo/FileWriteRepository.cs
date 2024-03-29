using RentVilla.Application.Repositories.FileRepo;
using File = RentVilla.Domain.Entities.Concrete.File;
using RentVilla.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Repositories.FileCRepo
{
    public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
    {
        public FileWriteRepository(RentVillaDbContext context) : base(context)
        {
        }
    }
}
