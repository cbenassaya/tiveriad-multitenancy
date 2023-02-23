using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Application.Queries.OrganizationQueries;
public class GetAllOrganizationsPreValidator : AbstractValidator<GetAllOrganizationsRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public GetAllOrganizationsPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}