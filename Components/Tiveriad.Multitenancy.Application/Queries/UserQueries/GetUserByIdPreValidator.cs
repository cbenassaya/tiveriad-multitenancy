using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Queries.UserQueries;
public class GetUserByIdPreValidator : AbstractValidator<GetUserByIdRequest>
{
    private IRepository<User, string> _userRepository;
    public GetUserByIdPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}