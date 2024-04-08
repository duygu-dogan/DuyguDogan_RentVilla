using RentVilla.Domain.Entities.Concrete.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Repositories.ReservationCartRepo
{
    public interface IResCartWriteRepository: IWriteRepository<ReservationCart>
    {
    }
}
