using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Tiveriad.Multitenancy.Api.EndPoints.UserEndPoints;
public class DeleteEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public DeleteEndPoint(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpDelete("/api/users/{id}")]
    public async Task<ActionResult<bool>> HandleAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var result = await _mediator.Send(new DeleteUserByIdRequest(id), cancellationToken);
        //<-- END CUSTOM CODE-->
        return Ok(result);
    }
}