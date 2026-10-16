using CleanArchitectureCQRS.Application.DTOs;
using CleanArchitectureCQRS.Application.Queries;
using CleanArchitectureCQRS.Infrastructure.Persistence;
using MediatR;

namespace CleanArchitectureCQRS.Application.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetProductByIdQueryHandler> _logger;

    public GetProductByIdQueryHandler(IApplicationDbContext context, ILogger<GetProductByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Query handler: retrieving product with ID {ProductId}", request.Id);
        
        try
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Query handler: product with ID {ProductId} not found", request.Id);
                return null;
            }

            _logger.LogInformation("Query handler: successfully retrieved product {ProductName} with ID {ProductId}", product.Name, request.Id);
            
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Query handler: error retrieving product with ID {ProductId}", request.Id);
            throw;
        }
    }
}
