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
    public class ProductHubService : IProductHubService
    {
        private readonly IHubContext<ProductHub> _productHubContext;

        public ProductHubService(IHubContext<ProductHub> productHubContext)
        {
            _productHubContext = productHubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
           await _productHubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);  
        }
    }
}
