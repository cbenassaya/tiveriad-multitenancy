using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Tiveriad.Multitenancy.Application.Commands.OrganizationCommands;
public class DeleteOrganizationByIdRequestHandler : IRequestHandler<DeleteOrganizationByIdRequest, bool>
{
    private readonly IRepository<Organization, string> _organizationRepository;
    public DeleteOrganizationByIdRequestHandler(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public Task<bool> Handle(DeleteOrganizationByIdRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        return Task.Run(() => _organizationRepository.DeleteMany(x => x.Id == request.Id) == 1, cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}