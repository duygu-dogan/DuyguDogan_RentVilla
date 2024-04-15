using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;

namespace RentVilla.Application.Feature.Queries.Reservations.GetActiveReservations
{
    public class GetActiveReservationsQueryHandler : IRequestHandler<GetActiveReservationsQueryRequest, GetActiveReservationsQueryResponse>
    {
        private readonly IReservationService _reservationService;
        readonly ILogger<GetActiveReservationsQueryHandler> _logger;

        public GetActiveReservationsQueryHandler(IReservationService reservationService, ILogger<GetActiveReservationsQueryHandler> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        public async Task<GetActiveReservationsQueryResponse> Handle(GetActiveReservationsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _reservationService.GetActiveReservations();
            response = response.Skip(request.Page * request.Size).Take(request.Size).ToList();
            if (response != null)
            {   
                return new GetActiveReservationsQueryResponse
                {
                    activeReservations = response
                };
            }
            else
            {
                _logger.LogError("No active reservations found");
                return new GetActiveReservationsQueryResponse
                {
                    activeReservations = null
                };
            }
        }
    }
}
