using Tiveriad.EnterpriseIntegrationPatterns.MessageBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Workers.Subscribers;

namespace Tiveriad.Multitenancy.Workers;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly MembershipDomainEventSubscriber _subscriber;

    public Worker(ILogger<Worker> logger, MembershipDomainEventSubscriber subscriber)
    {
        _logger = logger;
        _subscriber = subscriber;
        _subscriber.Subscribe();
    }
   

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}