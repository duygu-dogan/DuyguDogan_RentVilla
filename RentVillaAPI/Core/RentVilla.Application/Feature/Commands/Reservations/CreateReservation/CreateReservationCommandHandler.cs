using MediatR;
using RentVilla.Application.Abstraction.Hubs;
using RentVilla.Application.Abstraction.Iyzico;
using RentVilla.Application.Abstraction.Services;

namespace RentVilla.Application.Feature.Commands.Reservations.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommandRequest, CreateReservationCommandResponse>
    {
        private readonly IReservationService _reservationService;
        private readonly IPaymentService _paymentService;
        private readonly IReservationHubService _reservationHubService;

        public CreateReservationCommandHandler(IReservationService reservationService, IReservationHubService reservationHubService, IPaymentService paymentService)
        {
            _reservationService = reservationService;
            _reservationHubService = reservationHubService;
            _paymentService = paymentService;
        }

        public async Task<CreateReservationCommandResponse> Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            await _paymentService.CompletePaymentAsync(request.createReservation);
            await _reservationHubService.ReservationCreatedMessageAsync($"There is a new reservation on {request.createReservation.ProductName}!");
            return new();
        }
    }
}
