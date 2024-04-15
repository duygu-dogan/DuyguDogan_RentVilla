using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Reservations.GetPassiveReservations
{
    public class GetPassiveReservationsQueryRequest: IRequest<GetPassiveReservationsQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}
