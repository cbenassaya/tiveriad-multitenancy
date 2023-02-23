using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Queries.OrganizationQueries;
public class GetAllOrganizationsPreValidator : AbstractValidator<GetAllOrganizationsRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public GetAllOrganizationsPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}