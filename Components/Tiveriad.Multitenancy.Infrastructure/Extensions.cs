using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Tiveriad.Commons.RetryLogic;
using Tiveriad.Connections;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.EnterpriseIntegrationPatterns.MessageBrokers;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Multitenancy.Core.Services;
using Tiveriad.Multitenancy.Infrastructure.Publishers;
using Tiveriad.Multitenancy.Infrastructure.Services;
using Tiveriad.Multitenancy.Persistence;
using Tiveriad.Repositories.EntityFrameworkCore.Repositories;
using Tiveriad.Repositories.Microsoft.DependencyInjection;

namespace Tiveriad.Multitenancy.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, DefaultContext>(options =>
        {
            var logger = services.BuildServiceProvider().GetService<ILogger<DefaultContext>>();
            if (logger!=null) 
                options.LogTo(message => { logger.LogInformation(message); }).EnableSensitiveDataLogging().EnableDetailedErrors();
            options.UseSqlite("Data Source=multi-tenancy.db");
        });
        services.AddRepositories(typeof(EFRepository<, >), typeof(Organization));
        services.AddTransient<IUserManagerService, UserManagerService>();
        services.AddSingleton<IPublisher<UserDomainEvent, string>>(sp => new UserDomainEventPublisher(
            sp.GetRequiredService<IConnectionFactory<IConnection>>(),
            sp.GetRequiredService<IRabbitMqConnectionConfiguration>(),
            typeof(UserDomainEvent).FullName,
            sp.GetRequiredService<ILogger<UserDomainEventPublisher>>()));
        services.AddSingleton<IPublisher<OrganizationDomainEvent, string>>(sp => new OrganizationDomainEventPublisher(
            sp.GetRequiredService<IConnectionFactory<IConnection>>(),
            sp.GetRequiredService<IRabbitMqConnectionConfiguration>(),
            typeof(OrganizationDomainEvent).FullName,
            sp.GetRequiredService<ILogger<OrganizationDomainEventPublisher>>()));
        services.AddSingleton<IPublisher<MembershipDomainEvent, string>>(sp => new MembershipDomainEventPublisher(
            sp.GetRequiredService<IConnectionFactory<IConnection>>(),
            sp.GetRequiredService<IRabbitMqConnectionConfiguration>(),
            typeof(MembershipDomainEvent).FullName,
            sp.GetRequiredService<ILogger<MembershipDomainEventPublisher>>()));
        return services;
    }
}


public class RabbitMqPublisher2<TEvent, TKey> : IPublisher<TEvent, TKey>
    where TEvent : IDomainEvent<TKey>
    where TKey : IEquatable<TKey>
  {
    private readonly IConnectionFactory<IConnection> _connectionFactory;
    private readonly ILogger<RabbitMqPublisher2<TEvent, TKey>> _logger;
    private readonly IRabbitMqConnectionConfiguration _configuration;
    private readonly string _eventName;

    public RabbitMqPublisher2(
      IConnectionFactory<IConnection> connectionFactory,
      IRabbitMqConnectionConfiguration configuration,
      string eventName,
      ILogger<RabbitMqPublisher2<TEvent, TKey>> logger)
    {
      this._connectionFactory = connectionFactory;
      this._logger = logger;
      this._eventName = eventName;
      this._configuration = configuration;
    }

    public Task Publish(IDomainEvent<TKey> @event, CancellationToken cancellationToken)
    {
        IConnection connection = this._connectionFactory.GetConnection();

    connection.CallbackException += (e, context) =>
    {
        this._logger.LogCritical(e.ToString());
        this._logger.LogCritical(context.ToString());

    };
      IModel channel = connection.CreateModel();
      this._logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", (object) @event.Id);
      channel.ExchangeDeclare(this._configuration.BrokerName, "direct", true);
      byte[] body = JsonSerializer.SerializeToUtf8Bytes((object) @event, @event.GetType(), new JsonSerializerOptions()
      {
        WriteIndented = true
      });
      Retry.On<Exception>().For(3U).Execute((Action<RetryContext>) (context =>
      {
        if (context.Exceptions.Count > 1)
          connection = this._connectionFactory.GetConnection();
        IBasicProperties basicProperties = channel.CreateBasicProperties();
        basicProperties.DeliveryMode = (byte) 2;
        this._logger.LogTrace("Publishing event to RabbitMQ: {EventId}", (object) @event.Id);
        channel.BasicPublish(this._configuration.BrokerName, this._eventName, true, basicProperties, (ReadOnlyMemory<byte>) body);
      }));
      return Task.CompletedTask;
    }
  }