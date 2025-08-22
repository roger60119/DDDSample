using Microsoft.AspNetCore.Mvc;
using MediatR;
using DDDSample.Application.DTOs;

namespace DDDSample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        => Ok(await _mediator.Send(new GetAllOrdersQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(long id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> PostOrder(OrderDto dto)
    {
        var created = await _mediator.Send(new CreateOrderCommand(dto));
        return CreatedAtAction(nameof(GetOrder), new { id = created.OrderId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(long id, OrderDto dto)
    {
        if (id != dto.OrderId) return BadRequest();
        var updated = await _mediator.Send(new UpdateOrderCommand(id, dto));
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(long id)
    {
        var deleted = await _mediator.Send(new DeleteOrderCommand(id));
        if (!deleted) return NotFound();
        return NoContent();
    }
}
