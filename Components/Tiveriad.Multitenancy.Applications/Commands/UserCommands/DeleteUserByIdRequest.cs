using MediatR;
using System;
using Tiveriad.Multitenancy.Application.Commands;

public record DeleteUserByIdRequest(string Id) : IRequest<bool>,ICommandRequest;