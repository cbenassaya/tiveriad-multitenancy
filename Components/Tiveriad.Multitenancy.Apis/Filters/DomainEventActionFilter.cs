using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.EnterpriseIntegrationPatterns.MessageBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Exceptions;

namespace Tiveriad.Multitenancy.Api.Filters;

public class DomainEventActionFilter : IAsyncActionFilter
{
    private readonly IDomainEventStore _store;
    private readonly IPublisher<UserDomainEvent, string> _userDomainEventPublisher;
    private readonly IPublisher<MembershipDomainEvent, string> _membershipDomainEventPublisher;
    private readonly IPublisher<OrganizationDomainEvent, string> _organizationDomainEventPublisher;

    public DomainEventActionFilter(IDomainEventStore store, IPublisher<UserDomainEvent, string> userDomainEventPublisher, IPublisher<MembershipDomainEvent, string> membershipDomainEventPublisher, IPublisher<OrganizationDomainEvent, string> organizationDomainEventPublisher)
    {
        _store = store;
        _userDomainEventPublisher = userDomainEventPublisher;
        _membershipDomainEventPublisher = membershipDomainEventPublisher;
        _organizationDomainEventPublisher = organizationDomainEventPublisher;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = await next();
        if (result.Exception == null || result.ExceptionHandled)
        {
            _store.Commit();
            
            foreach (var entry in _store.Entries<UserDomainEvent, string>())
                await _userDomainEventPublisher.Publish(entry);
            
            foreach (var entry in _store.Entries<MembershipDomainEvent, string>())
                await _membershipDomainEventPublisher.Publish(entry);
            
            foreach (var entry in _store.Entries<OrganizationDomainEvent, string>())
                await _organizationDomainEventPublisher.Publish(entry);
        }
    }
}

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid) context.Result = new BadRequestObjectResult(context.ModelState);
    }
}

public class ApiExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var multiTenancyException = context.Exception as MultiTenancyException;
        var logger =
            context.HttpContext.RequestServices.GetService(typeof(ILogger<ApiExceptionFilter>)) as
                ILogger<ApiExceptionFilter>;


        logger?.LogError(context.Exception.Message, context.Exception, context.HttpContext.Request);

        if (multiTenancyException != null)

            context.Result = new BadRequestObjectResult(multiTenancyException.Message);
        else
            context.Result = new ObjectResult(context.Exception.Message)
                { StatusCode = StatusCodes.Status500InternalServerError };

        return Task.CompletedTask;
    }
}