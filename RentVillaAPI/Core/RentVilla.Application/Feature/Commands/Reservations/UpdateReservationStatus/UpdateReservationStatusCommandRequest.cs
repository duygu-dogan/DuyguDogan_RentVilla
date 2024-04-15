using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.Reservations.UpdateReservationStatus
{
    public class UpdateReservationStatusCommandRequest: IRequest<UpdateReservationStatusCommandResponse>
    {
        public string ReservationId { get; set; }
        public string Status { get; set; }
    }
}
