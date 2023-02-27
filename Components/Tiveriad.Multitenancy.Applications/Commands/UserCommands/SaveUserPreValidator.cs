using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;
public class SaveUserPreValidator : AbstractValidator<SaveUserRequest>
{
    private IRepository<User, string> _userRepository;
    public SaveUserPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}