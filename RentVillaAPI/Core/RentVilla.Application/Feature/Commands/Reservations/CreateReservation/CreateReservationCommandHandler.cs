using MediatR;
using RentVilla.Application.Abstraction.Hubs;
using RentVilla.Application.Abstraction.Services;

namespace RentVilla.Application.Feature.Commands.Reservations.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommandRequest, CreateReservationCommandResponse>
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationHubService _reservationHubService;

        public CreateReservationCommandHandler(IReservationService reservationService, IReservationHubService reservationHubService)
        {
            _reservationService = reservationService;
            _reservationHubService = reservationHubService;
        }

        public async Task<CreateReservationCommandResponse> Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
           await _reservationService.CreateReservationAsync(request.createReservation);
            await _reservationHubService.ReservationCreatedMessageAsync("There is a new reservation!");
            return new();
        }
    }
}
