using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Queries.OrganizationQueries;
public class GetOrganizationByIdPreValidator : AbstractValidator<GetOrganizationByIdRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public GetOrganizationByIdPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}