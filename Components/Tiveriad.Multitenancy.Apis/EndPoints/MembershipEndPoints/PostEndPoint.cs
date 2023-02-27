using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tiveriad.Multitenancy.Apis.Contracts;
using Tiveriad.Multitenancy.Apis.Filters;
using Tiveriad.Multitenancy.Applications.Commands.MembershipCommands;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Apis.EndPoints.MembershipEndPoints;
public class PostEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public PostEndPoint(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/api/memberships")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ValidateModel]
    public async Task<ActionResult<MembershipReaderModel>> HandleAsync([FromBody] MembershipWriterModel model, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var entity = _mapper.Map<MembershipWriterModel, Membership>(model);
        var result = await _mediator.Send(new SaveMembershipRequest(entity), cancellationToken);
        var data = _mapper.Map<Membership, MembershipReaderModel>(result);
        //<-- END CUSTOM CODE-->
        return Ok(data);
    }
}