using MediatR;
using Tiveriad.Multitenancy.Application.Commands;
using Tiveriad.Multitenancy.Core.Entities;

public record SaveOrUpdateMembershipRequest(Membership Membership) : IRequest<Membership>,ICommandRequest;