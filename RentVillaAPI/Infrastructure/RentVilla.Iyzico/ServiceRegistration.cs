using Microsoft.Extensions.DependencyInjection;
using RentVilla.Application.Abstraction.Iyzico;
using RentVilla.Iyzico.IyzicoServices;

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
