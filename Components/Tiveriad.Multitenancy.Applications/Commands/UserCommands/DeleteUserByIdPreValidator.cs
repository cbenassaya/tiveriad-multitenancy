using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Commands.UserCommands;
public class DeleteUserByIdPreValidator : AbstractValidator<DeleteUserByIdRequest>
{
    private IRepository<User, string> _userRepository;
    public DeleteUserByIdPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}