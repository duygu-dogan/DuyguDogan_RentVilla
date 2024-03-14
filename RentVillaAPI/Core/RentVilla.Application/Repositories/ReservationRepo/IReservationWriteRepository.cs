using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Repositories.ReservationRepo
{
    public interface IReservationWriteRepository: IWriteRepository<Reservation>
    {
    }
}
