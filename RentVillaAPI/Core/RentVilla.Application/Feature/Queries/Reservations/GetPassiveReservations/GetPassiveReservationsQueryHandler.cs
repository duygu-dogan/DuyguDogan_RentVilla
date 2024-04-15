using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Domain.Entities.Concrete;

namespace RentVilla.Application.Feature.Queries.Reservations.GetPassiveReservations
{
    public class GetPassiveReservationsQueryHandler : IRequestHandler<GetPassiveReservationsQueryRequest, GetPassiveReservationsQueryResponse>
    {
        private readonly IReservationService _reservationService;
        readonly ILogger<GetPassiveReservationsQueryHandler> _logger;

        public GetPassiveReservationsQueryHandler(IReservationService reservationService, ILogger<GetPassiveReservationsQueryHandler> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        public async Task<GetPassiveReservationsQueryResponse> Handle(GetPassiveReservationsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _reservationService.GetPassiveReservations();
            response = response.Skip(request.Page * request.Size).Take(request.Size).ToList();
            if (response != null)
            {   
                return new GetPassiveReservationsQueryResponse
                {
                    passiveReservations = response
                };
            }
            else
            {
                _logger.LogError("No active reservations found");
                return new GetPassiveReservationsQueryResponse
                {
                    passiveReservations = null
                };
            }
        }
    }
}
