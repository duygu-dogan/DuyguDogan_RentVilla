using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentVilla.Persistence.ScheduledTasks;

namespace RentVilla.ScheduleTask
{
    public static class ServiceRegistration
    {
        public static void AddScheduledTaskServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHostedService, ReservationStatusTask>();
        }
    }
}
