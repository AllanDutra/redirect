using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Redirect.Core.Services;
using Redirect.Core.Repositories;

namespace Redirect.Application.Services
{
    public class ExpirationCheckerService : IExpirationCheckerService
    {
        private readonly IRedirectRepository _redirectRepository;
        public ExpirationCheckerService(IRedirectRepository redirectRepository)
        {
            _redirectRepository = redirectRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            await _redirectRepository.DeleteExpiredsShortenedsUrlAsync();
        }
    }

    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        private const int INTERVAL_TO_CHECK_EXPIRATION = 10000;
        private readonly IServiceProvider _services;
        public ConsumeScopedServiceHostedService(IServiceProvider services)
        {
            _services = services;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(INTERVAL_TO_CHECK_EXPIRATION, stoppingToken);

                using (var scope = _services.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IExpirationCheckerService>();

                    await scopedProcessingService.DoWork(stoppingToken);
                }
            }
        }
    }
}