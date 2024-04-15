using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Repositories.ReservationRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Reservations.CancelReservation
{
    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommandRequest, CancelReservationCommandResponse>
    {
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly ILogger<CancelReservationCommandHandler> _logger;

        public CancelReservationCommandHandler(IReservationWriteRepository reservationWriteRepository, ILogger<CancelReservationCommandHandler> logger)
        {
            _reservationWriteRepository = reservationWriteRepository;
            _logger = logger;
        }

        public async Task<CancelReservationCommandResponse> Handle(CancelReservationCommandRequest request, CancellationToken cancellationToken)
        {
            await _reservationWriteRepository.DeleteAsync(request.ReservationId);
            await _reservationWriteRepository.SaveAsync();
            return new CancelReservationCommandResponse();
        }
    }
}
