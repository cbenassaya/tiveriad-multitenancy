using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using Tiveriad.Multitenancy.Api.Contracts;
using System.Threading;
using Tiveriad.Multitenancy.Api.Filters;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.EndPoints.MembershipEndPoints;
public class SaveOrUpdateEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public SaveOrUpdateEndPoint(IMapper mapper, IMediator mediator)
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
        var result = await _mediator.Send(new SaveOrUpdateMembershipRequest(entity), cancellationToken);
        var data = _mapper.Map<Membership, MembershipReaderModel>(result);
        //<-- END CUSTOM CODE-->
        return Ok(data);
    }
}