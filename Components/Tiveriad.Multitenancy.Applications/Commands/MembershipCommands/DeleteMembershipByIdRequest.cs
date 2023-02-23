using MediatR;
using System;
using Tiveriad.Multitenancy.Application.Commands;

public record DeleteMembershipByIdRequest(string Id) : IRequest<bool>,ICommandRequest;