using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Commands.UserCommands;
public class SaveOrUpdateUserPreValidator : AbstractValidator<SaveOrUpdateUserRequest>
{
    private IRepository<User, string> _userRepository;
    public SaveOrUpdateUserPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}