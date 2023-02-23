using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Queries.UserQueries;
public class GetAllUsersPreValidator : AbstractValidator<GetAllUsersRequest>
{
    private IRepository<User, string> _userRepository;
    public GetAllUsersPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}