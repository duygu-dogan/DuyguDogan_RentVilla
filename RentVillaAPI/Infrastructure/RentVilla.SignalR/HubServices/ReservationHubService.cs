using Microsoft.AspNetCore.SignalR;
using RentVilla.Application.Abstraction.Hubs;
using RentVilla.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.SignalR.HubServices
{
    public class ReservationHubService : IReservationHubService
    {
        readonly IHubContext<ReservationHub> _hubContext;

        public ReservationHubService(IHubContext<ReservationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ReservationCreatedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ReservationCreatedMessage, message);
        }
    }
}
