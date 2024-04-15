using RentVilla.Application.DTOs.ReservationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Reservations.GetActiveReservations
{
    public class GetActiveReservationsQueryResponse
    {
        public List<GetReservationDTO> activeReservations { get; set; }
    }
}
