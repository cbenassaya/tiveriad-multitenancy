using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Queries.OrganizationQueries;
public class GetOrganizationByIdPreValidator : AbstractValidator<GetOrganizationByIdRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public GetOrganizationByIdPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}