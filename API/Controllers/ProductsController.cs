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
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all products");
        var query = new GetAllProductsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        _logger.LogInformation("Retrieved {ProductCount} products", result.Count);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching product with ID {ProductId}", id);
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Retrieved product {ProductName} with ID {ProductId}", result.Name, id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new product with name {ProductName} and price {ProductPrice}", request.Name, request.Price);

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            _logger.LogWarning("Product creation failed: name is required");
            return BadRequest("Product name is required.");
        }

        if (request.Price <= 0)
        {
            _logger.LogWarning("Product creation failed: price must be greater than zero");
            return BadRequest("Product price must be greater than zero.");
        }

        var command = new CreateProductCommand(request.Name, request.Price);
        var result = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Product {ProductId} created successfully with name {ProductName}", result.Id, result.Name);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }
}
