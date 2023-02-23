using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Tiveriad.Connections;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;

namespace Tiveriad.Multitenancy.Infrastructure.Publishers;

public class UserDomainEventPublisher: RabbitMqPublisher2<UserDomainEvent, string>
{
    public UserDomainEventPublisher(
        IConnectionFactory<IConnection> connectionFactory,
        IRabbitMqConnectionConfiguration configuration,
        string eventName,
        ILogger<UserDomainEventPublisher> logger) : base(connectionFactory, configuration, eventName, logger)
    {
    }
}