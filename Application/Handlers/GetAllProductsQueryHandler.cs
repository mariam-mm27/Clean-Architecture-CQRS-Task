using CleanArchitectureCQRS.Application.DTOs;
using CleanArchitectureCQRS.Application.Queries;
using CleanArchitectureCQRS.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureCQRS.Application.Handlers;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetAllProductsQueryHandler> _logger;

    public GetAllProductsQueryHandler(IApplicationDbContext context, ILogger<GetAllProductsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Query handler: retrieving all products from database");
        
        try
        {
            var products = await _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToListAsync(cancellationToken);
            
            _logger.LogInformation("Query handler: successfully retrieved {ProductCount} products", products.Count);
            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Query handler: error retrieving products");
            throw;
        }
    }
}
