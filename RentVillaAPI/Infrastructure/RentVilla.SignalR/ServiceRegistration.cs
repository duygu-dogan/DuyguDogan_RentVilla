using Microsoft.Extensions.DependencyInjection;
using RentVilla.Application.Abstraction.Hubs;
using RentVilla.SignalR.HubServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddTransient<IReservationHubService, ReservationHubService>();
            collection.AddSignalR();
        }
    }
}
