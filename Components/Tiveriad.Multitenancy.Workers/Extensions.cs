using RabbitMQ.Client;
using Tiveriad.Connections;
using Tiveriad.EnterpriseIntegrationPatterns.DependencyInjection;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.EnterpriseIntegrationPatterns.MessageBrokers;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq;
using Tiveriad.Keycloak;
using Tiveriad.Keycloak.Apis;
using Tiveriad.Keycloak.Services;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Services;
using Tiveriad.Multitenancy.Infrastructure.Services;
using Tiveriad.Multitenancy.Workers.Subscribers;
using Tiveriad.ServiceResolvers;

namespace Tiveriad.Multitenancy.Workers;

public static class Extensions
{
    public static IServiceCollection AddMultiTenancyWorkerServices(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
        return services;
    }

    public static void AddKeycloak(this IServiceCollection services)
    {
        services.AddSingleton<KeycloakConfigurationService>();
        var configuration = services
            .BuildServiceProvider()
            .GetRequiredService<KeycloakConfigurationService>();
        UriBuilder uriBuilder = new UriBuilder
        {
            Scheme = configuration.Scheme,
            Host = configuration.Hostname,
        };
        
        var factory = KeycloakSessionFactory.Configurator.Get(x =>
        {
            x.SetCredential(configuration.Username, configuration.Password).SetUrlBase(uriBuilder.Uri.ToString());
        }).Build();

        services.AddSingleton(factory);
        
        
        services.AddSingleton( x =>
        {
            var httpClient = new HttpClient();
          
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = configuration.Scheme,
                Host = configuration.Hostname,
                Path = configuration.Path
            };
            httpClient.BaseAddress = uriBuilder.Uri;
            return httpClient;
        });
        
        services.AddSingleton<IRoleApi, RoleApi>();
        services.AddSingleton<IUserApi, UserApi>();
        services.AddSingleton<IRoleMapperApi, RoleMapperApi>();
        services.AddSingleton<IIdentityService, KeycloakIdentityService>();
    }

    public static void AddEip(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqConfigurationService>();
        var configuration = services
            .BuildServiceProvider()
            .GetRequiredService<RabbitMqConfigurationService>();
        
        services.ConfigureConnectionFactory<RabbitMqConnectionFactoryBuilder, IConnection, RabbitMqConnectionConfigurator,
            IRabbitMqConnectionConfiguration>
        ( configurator =>
            {
                configurator
                    .SetHost(configuration.Hostname)
                    .SetUsername(configuration.Username)
                    .SetPassword(configuration.Password)
                    .SetBrokerName(configuration.Exchange);
            }
        );
        services.AddScoped<IServiceResolver, DependencyInjectionServiceResolver>();
        services.AddTiveriadEip(typeof(MembershipDomainEvent).Assembly);
        services.AddScoped<IDomainEventStore, DomainEventStore>();
    }
    
    public static void AddSubscribers(this IServiceCollection services)
    {

        services.AddSingleton(sp => new MembershipDomainEventSubscriber(
            sp.GetRequiredService<IConnectionFactory<IConnection>>(),
            sp.GetRequiredService<IRabbitMqConnectionConfiguration>(),
            $"{typeof(MembershipDomainEvent).FullName}Queue",
            typeof(MembershipDomainEvent).FullName,
            sp.GetRequiredService<IIdentityService>(),
            sp.GetRequiredService<ILogger<MembershipDomainEventSubscriber>>()));
    }
}