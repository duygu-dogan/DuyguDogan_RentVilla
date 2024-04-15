using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Reservations.CancelReservation
{
    public class CancelReservationCommandRequest: IRequest<CancelReservationCommandResponse>
    {
        public string ReservationId { get; set; }
    }
}
