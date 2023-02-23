using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Application.Commands.UserCommands;
public class SaveOrUpdateUserPreValidator : AbstractValidator<SaveOrUpdateUserRequest>
{
    private IRepository<User, string> _userRepository;
    public SaveOrUpdateUserPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}