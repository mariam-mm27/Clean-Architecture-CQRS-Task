using CleanArchitectureCQRS.Application.Commands;
using CleanArchitectureCQRS.Application.DTOs;
using CleanArchitectureCQRS.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureCQRS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts(CancellationToken cancellationToken)
    {
        var query = new GetAllProductsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Product name is required.");

        if (request.Price <= 0)
            return BadRequest("Product price must be greater than zero.");

        var command = new CreateProductCommand(request.Name, request.Price);
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }
}
