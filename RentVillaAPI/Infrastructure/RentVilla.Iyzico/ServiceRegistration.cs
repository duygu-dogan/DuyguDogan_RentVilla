using Microsoft.Extensions.DependencyInjection;
using RentVilla.Application.Abstraction.Iyzico;
using RentVilla.Iyzico.IyzicoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Iyzico
{
    public static class ServiceRegistration
    {
        public static void AddIyzicoServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPaymentService, PaymentService>();   
        }
    }
}
