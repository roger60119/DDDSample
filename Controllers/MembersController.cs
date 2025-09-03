using Microsoft.AspNetCore.Mvc;
using MediatR;
using DDDSample.Application.DTOs;
using DDDSample.Attributes;

namespace DDDSample.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiKeyAuth]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        => Ok(await _mediator.Send(new GetAllMembersQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetMember(int id)
    {
        var member = await _mediator.Send(new GetMemberByIdQuery(id));
        if (member == null) return NotFound();
        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<MemberDto>> PostMember(MemberDto dto)
    {
        var created = await _mediator.Send(new CreateMemberCommand(dto));
        return CreatedAtAction(nameof(GetMember), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMember(int id, MemberDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var updated = await _mediator.Send(new UpdateMemberCommand(id, dto));
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var deleted = await _mediator.Send(new DeleteMemberCommand(id));
        if (!deleted) return NotFound();
        return NoContent();
    }
}
