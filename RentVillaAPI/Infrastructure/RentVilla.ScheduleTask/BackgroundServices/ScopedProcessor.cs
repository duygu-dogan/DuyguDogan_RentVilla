using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.BackgroundServices
{
    public abstract class ScopedProcessor : BackgroundService
    {
        IServiceScopeFactory _serviceScopeFactory;
        protected ScopedProcessor(ILogger<BackgroundService> logger, IServiceScopeFactory serviceScopeFactory) : base(logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task Process()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                await ProcessInScope(scope.ServiceProvider);
            }
        }
        protected abstract Task ProcessInScope(IServiceProvider serviceProvider);
    }
}
