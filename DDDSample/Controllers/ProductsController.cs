using Microsoft.AspNetCore.Mvc;
using MediatR;
using DDDSample.Application.DTOs;
using DDDSample.Attributes;
using DDDSample.Application.Queries.Products;
using DDDSample.Application.Commands.Products;

namespace DDDSample.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiKeyAuth]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        => Ok(await _mediator.Send(new GetAllProductsQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProduct(ProductDto dto)
    {
        var created = await _mediator.Send(new CreateProductCommand(dto));
        return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var updated = await _mediator.Send(new UpdateProductCommand(id, dto));
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _mediator.Send(new DeleteProductCommand(id));
        if (!deleted) return NotFound();
        return NoContent();
    }
}