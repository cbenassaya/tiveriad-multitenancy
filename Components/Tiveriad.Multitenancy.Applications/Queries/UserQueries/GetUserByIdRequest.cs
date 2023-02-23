using MediatR;
using Tiveriad.Multitenancy.Core.Entities;
using System;

public record GetUserByIdRequest(string Id) : IRequest<User>;