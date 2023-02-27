using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;

public class UpdateUserPreValidator : AbstractValidator<UpdateUserRequest>
{
    private IRepository<User, string> _userRepository;
    public UpdateUserPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}