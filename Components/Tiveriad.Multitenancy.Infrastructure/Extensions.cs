using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tiveriad.Multitenancy.Persistence;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Multitenancy.Core.Services;
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
        services.AddTransient<IPublishService, PublishService>();
        return services;
    }
}