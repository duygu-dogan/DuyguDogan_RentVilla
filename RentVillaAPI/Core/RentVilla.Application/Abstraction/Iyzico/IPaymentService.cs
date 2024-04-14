using RentVilla.Application.DTOs.ReservationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstraction.Iyzico
{
    public interface IPaymentService
    {
        Task CompletePaymentAsync(CreateReservationDTO reservationDTO);
    }
}
