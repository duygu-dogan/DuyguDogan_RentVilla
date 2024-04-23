using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RentVilla.Persistence.BackgroundServices
{
    public abstract class BackgroundService: IHostedService
    {
        private Task _executingTask;
        protected ILogger<BackgroundService> _logger;

        protected BackgroundService(ILogger<BackgroundService> logger)
        {
            _logger = logger;
        }

        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            if(_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            _logger.LogInformation("CheckReservationStatusService is started.");
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if(_executingTask == null)
            {
                return;
            }
            try
            {
                _stoppingCts.Cancel();
            }
            catch (Exception ex)
            {

                throw;
            }finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }
        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                await Process();

                await Task.Delay(5000, stoppingToken);
            }while(!stoppingToken.IsCancellationRequested);
        }
        protected abstract Task Process();
    }
}
