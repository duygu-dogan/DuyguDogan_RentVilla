using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Repositories.ReservationRepo;
using RentVilla.Domain.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Reservations.UpdateReservationStatus
{
    public class UpdateReservationStatusCommandHandler: IRequestHandler<UpdateReservationStatusCommandRequest, UpdateReservationStatusCommandResponse>
    {
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly IReservationReadRepository _reservationReadRepository;
        readonly ILogger<UpdateReservationStatusCommandHandler> _logger;

        public UpdateReservationStatusCommandHandler(IReservationWriteRepository reservationWriteRepository, IReservationReadRepository reservationReadRepository, ILogger<UpdateReservationStatusCommandHandler> logger)
        {
            _reservationWriteRepository = reservationWriteRepository;
            _reservationReadRepository = reservationReadRepository;
            _logger = logger;
        }

        public async Task<UpdateReservationStatusCommandResponse> Handle(UpdateReservationStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationReadRepository.GetByIdAsync(request.ReservationId);
            bool result = Enum.TryParse(request.Status, true, out ReservationStatusType reservationStatus);
            if (result)
            {
                reservation.Status = reservationStatus;
                await _reservationWriteRepository.SaveAsync();
                return new UpdateReservationStatusCommandResponse();
            }
            else
            {
                _logger.LogError("Invalid status value");
                throw new Exception("Invalid status value");
            }

        }
    }
}
