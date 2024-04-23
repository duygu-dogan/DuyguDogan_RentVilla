using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Repositories.ReservationRepo;
using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Persistance.Contexts;
using RentVilla.Persistence.BackgroundServices;

namespace RentVilla.Persistence.ScheduledTasks
{
    public class ReservationStatusTask : ScheduledProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private new readonly ILogger<ReservationStatusTask> _logger;
        public ReservationStatusTask(ILogger<ReservationStatusTask> logger, IServiceScopeFactory serviceScopeFactory) : base(logger, serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override string Schedule => "0 0 * * *";

        protected async override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RentVillaDbContext>();
                try
                {
                    var today = DateTime.UtcNow;
                    var reservationsToEnd = dbContext.Reservations.Where(r => r.EndDate < today && r.Status == ReservationStatusType.Open).ToList();
                    foreach (var reservation in reservationsToEnd)
                    {
                        reservation.Status = ReservationStatusType.Completed;
                        dbContext.Reservations.Entry(reservation).State = EntityState.Modified;
                    }
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation("Reservation status updated");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Reservation status update failed.");
                }

            }
        }
    }
}
