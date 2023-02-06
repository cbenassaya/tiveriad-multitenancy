using MediatR;
using Tiveriad.Multitenancy.Application.Commands;
using Tiveriad.Multitenancy.Core.Services;

namespace Tiveriad.Multitenancy.Application.Pipelines;

public class PublishEventBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IPublishService _publishService;

    public PublishEventBehaviour(IPublishService publishService)
    {
        _publishService = publishService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();
        if (request is ICommandRequest)
            _publishService.Publish<TRequest>(request);
        return response;
    }
}