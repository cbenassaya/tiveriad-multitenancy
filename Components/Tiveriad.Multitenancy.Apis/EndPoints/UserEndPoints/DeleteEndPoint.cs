using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HandleAsync([FromRoute][Required] string id, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        if (string.IsNullOrEmpty(id))
            return BadRequest("Id is mandatory");
        var result = await _mediator.Send(new DeleteUserByIdRequest(id), cancellationToken);
        //<-- END CUSTOM CODE-->
        return Ok(result);
    }
}