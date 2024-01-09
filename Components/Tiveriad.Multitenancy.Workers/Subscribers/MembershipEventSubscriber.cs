using RabbitMQ.Client;
using Tiveriad.Connections;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Services;

namespace Tiveriad.Multitenancy.Workers.Subscribers;


public class MembershipDomainEventSubscriber: RabbitMqSubscriber<MembershipDomainEvent, string>
{   
    private readonly ILogger<MembershipDomainEventSubscriber> _logger;
    private readonly IIdentityService _identityService;
    public MembershipDomainEventSubscriber(
        IConnectionFactory<IConnection> connectionFactory,
        IRabbitMqConnectionConfiguration configuration,
        string queueName,
        string eventName,
        IIdentityService identityService,
        ILogger<MembershipDomainEventSubscriber> logger) : base(connectionFactory, configuration,queueName, eventName, logger)
    {
        _identityService = identityService;
        _logger= logger;
    }

    public override Task OnError(Exception exception)
    {
        _logger.LogError("Error: {exception}", exception);
        return Task.CompletedTask;
    }

    public override Task Handle(MembershipDomainEvent membershipDomainEvent)
    {
        return _identityService.Update(membershipDomainEvent.Membership.User, membershipDomainEvent.Membership.Organization);
    }
}