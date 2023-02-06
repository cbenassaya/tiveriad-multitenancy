using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Tiveriad.Multitenancy.Api.EndPoints.OrganizationEndPoints;
public class DeleteEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public DeleteEndPoint(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpDelete("/api/organizations/{id}")]
    public async Task<ActionResult<bool>> HandleAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var result = await _mediator.Send(new DeleteOrganizationByIdRequest(id), cancellationToken);
        //<-- END CUSTOM CODE-->
        return Ok(result);
    }
}