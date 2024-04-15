using RentVilla.Application.DTOs.ReservationDTOs;

namespace RentVilla.Application.Abstraction.Services
{
    public interface IReservationService
    {
        Task CreateReservationAsync(CreateReservationDTO createReservation);
        Task<GetReservationDTO> GetReservationByIdAsync(string reservationId);
        Task<List<GetReservationDTO>> GetActiveReservations();
        Task<List<GetReservationDTO>> GetPassiveReservations();
        Task<List<GetReservationDTO>> GetReservationsByProductIdAsync (string productId);
    }
}
