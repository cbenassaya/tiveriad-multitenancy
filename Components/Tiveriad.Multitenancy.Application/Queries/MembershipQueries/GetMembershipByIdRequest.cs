using MediatR;
using Tiveriad.Multitenancy.Core.Entities;
using System;
using Tiveriad.Multitenancy.Application.Queries;

public record GetMembershipByIdRequest(string Id) : IRequest<Membership>, IQueryRequest;