using CleanArchitectureCQRS.Application.DTOs;
using CleanArchitectureCQRS.Application.Queries;
using CleanArchitectureCQRS.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureCQRS.Application.Handlers;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            })
            .ToListAsync(cancellationToken);
    }
}
