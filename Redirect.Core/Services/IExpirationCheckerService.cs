namespace Redirect.Core.Services
{
    public interface IExpirationCheckerService
    {
        public Task DoWork(CancellationToken stoppingToken);
    }
}