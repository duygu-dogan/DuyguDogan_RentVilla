using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstraction.Hubs
{
    public interface IReservationHubService
    {
        Task ReservationCreatedMessageAsync(string message);
    }
}
