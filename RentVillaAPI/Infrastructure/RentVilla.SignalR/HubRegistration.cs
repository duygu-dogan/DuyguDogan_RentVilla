using Microsoft.AspNetCore.Builder;
using RentVilla.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication app)
        {
            app.MapHub<ProductHub>("/product-hub");
        }
    }
}
