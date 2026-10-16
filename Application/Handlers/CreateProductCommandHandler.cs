using CleanArchitectureCQRS.Application.Commands;
using CleanArchitectureCQRS.Application.DTOs;
using CleanArchitectureCQRS.Domain.Entities;
using CleanArchitectureCQRS.Infrastructure.Persistence;
using MediatR;

namespace CleanArchitectureCQRS.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(IApplicationDbContext context, ILogger<CreateProductCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Command handler: creating product with name {ProductName} and price {ProductPrice}", request.Name, request.Price);
        
        try
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Command handler: product {ProductId} created successfully with name {ProductName}", product.Id, product.Name);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Command handler: error creating product with name {ProductName}", request.Name);
            throw;
        }
    }
}
