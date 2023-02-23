using MediatR;
using System;
using Tiveriad.Multitenancy.Application.Commands;

public record DeleteOrganizationByIdRequest(string Id) : IRequest<bool>,ICommandRequest;