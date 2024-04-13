
using RentVilla.Application.DTOs.Reservation;

namespace RentVilla.Application.Abstraction.Services
{
    public interface IReservationService
    {
        Task CreateReservationAsync(CreateReservationDTO createReservation);
    }
}
