using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Tiveriad.Connections;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;

namespace Tiveriad.Multitenancy.Infrastructure.Publishers;

public class MembershipDomainEventPublisher: RabbitMqPublisher2<MembershipDomainEvent, string>
{
    public MembershipDomainEventPublisher(
        IConnectionFactory<IConnection> connectionFactory,
        IRabbitMqConnectionConfiguration configuration,
        string eventName,
        ILogger<MembershipDomainEventPublisher> logger) : base(connectionFactory, configuration, eventName, logger)
    {
    }
}