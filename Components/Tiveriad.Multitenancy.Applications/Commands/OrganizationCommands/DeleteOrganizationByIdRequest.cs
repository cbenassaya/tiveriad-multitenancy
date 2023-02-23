using MediatR;
using Tiveriad.Multitenancy.Application.Commands;

public record DeleteOrganizationByIdRequest(string Id) : IRequest<bool>,ICommandRequest;