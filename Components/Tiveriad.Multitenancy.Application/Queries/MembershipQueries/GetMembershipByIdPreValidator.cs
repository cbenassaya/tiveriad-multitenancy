using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Queries.MembershipQueries;
public class GetMembershipByIdPreValidator : AbstractValidator<GetMembershipByIdRequest>
{
    private IRepository<Membership, string> _membershipRepository;
    private IRepository<User, string> _userRepository;
    public GetMembershipByIdPreValidator(IRepository<Membership, string> membershipRepository, IRepository<User, string> userRepository)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
    }
}