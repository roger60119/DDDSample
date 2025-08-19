using Microsoft.AspNetCore.Mvc;
using DDDSample.Application.Services;
using DDDSample.Application.DTOs;

namespace DDDSample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly MemberService _service;

    public MembersController(MemberService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetMember(int id)
    {
        var member = await _service.GetByIdAsync(id);
        if (member == null) return NotFound();
        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<MemberDto>> PostMember(MemberDto dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetMember), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMember(int id, MemberDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
