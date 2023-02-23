using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Queries.MembershipQueries;
public class GetAllMembershipPreValidator : AbstractValidator<GetAllMembershipRequest>
{
    private IRepository<Membership, string> _membershipRepository;
    private IRepository<User, string> _userRepository;
    public GetAllMembershipPreValidator(IRepository<Membership, string> membershipRepository, IRepository<User, string> userRepository)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
    }
}