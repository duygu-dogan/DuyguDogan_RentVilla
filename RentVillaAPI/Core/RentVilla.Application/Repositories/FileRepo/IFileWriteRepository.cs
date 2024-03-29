using F = RentVilla.Domain.Entities.Concrete;

namespace RentVilla.Application.Repositories.FileRepo
{
    public interface IFileWriteRepository: IWriteRepository<F::File>
    {
    }
}
