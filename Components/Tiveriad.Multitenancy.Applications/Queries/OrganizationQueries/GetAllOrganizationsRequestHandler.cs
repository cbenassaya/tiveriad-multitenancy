using MediatR;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Queries.OrganizationQueries;
public class GetAllOrganizationsRequestHandler : IRequestHandler<GetAllOrganizationsRequest, IEnumerable<Organization>>
{
    private readonly IRepository<Organization, string> _organizationRepository;
    public GetAllOrganizationsRequestHandler(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public Task<IEnumerable<Organization>> Handle(GetAllOrganizationsRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var query = _organizationRepository.Queryable;
        return Task.Run(() => query.ToList().AsEnumerable(), cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}