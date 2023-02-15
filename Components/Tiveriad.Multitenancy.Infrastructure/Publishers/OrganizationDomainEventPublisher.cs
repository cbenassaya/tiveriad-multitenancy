using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Tiveriad.Connections;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;

namespace Tiveriad.Multitenancy.Infrastructure.Publishers;

public class OrganizationDomainEventPublisher: RabbitMqPublisher<OrganizationDomainEvent, string>
{
    public OrganizationDomainEventPublisher(
        IConnectionFactory<IConnection> connectionFactory,
        IRabbitMqConnectionConfiguration configuration,
        string eventName,
        ILogger<OrganizationDomainEventPublisher> logger) : base(connectionFactory, configuration, eventName, logger)
    {
    }
}