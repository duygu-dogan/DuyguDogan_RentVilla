using RentVilla.Application.DTOs.ReservationDTOs;

namespace RentVilla.Application.Abstraction.Services
{
    public interface IReservationService
    {
        Task CreateReservationAsync(CreateReservationDTO createReservation);
    }
}
