using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Tiveriad.Connections;
using Tiveriad.EnterpriseIntegrationPatterns.MessageBrokers;
using Tiveriad.EnterpriseIntegrationPatterns.RabbitMq;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Persistence;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Multitenancy.Core.Services;
using Tiveriad.Multitenancy.Infrastructure.Publishers;
using Tiveriad.Multitenancy.Infrastructure.Services;
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
        services.AddScoped<IPublisher<UserDomainEvent, string>>(sp => new UserDomainEventPublisher(
            sp.GetRequiredService<IConnectionFactory<IConnection>>(),
            sp.GetRequiredService<IRabbitMqConnectionConfiguration>(),
            typeof(UserDomainEvent).FullName,
            sp.GetRequiredService<ILogger<UserDomainEventPublisher>>()));
        services.AddScoped<IPublisher<OrganizationDomainEvent, string>>(sp => new OrganizationDomainEventPublisher(
            sp.GetRequiredService<IConnectionFactory<IConnection>>(),
            sp.GetRequiredService<IRabbitMqConnectionConfiguration>(),
            typeof(OrganizationDomainEvent).FullName,
            sp.GetRequiredService<ILogger<OrganizationDomainEventPublisher>>()));
        services.AddScoped<IPublisher<MembershipDomainEvent, string>>(sp => new MembershipDomainEventPublisher(
            sp.GetRequiredService<IConnectionFactory<IConnection>>(),
            sp.GetRequiredService<IRabbitMqConnectionConfiguration>(),
            typeof(MembershipDomainEvent).FullName,
            sp.GetRequiredService<ILogger<MembershipDomainEventPublisher>>()));
        return services;
    }
}