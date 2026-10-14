using CleanArchitectureCQRS.Application.DTOs;
using CleanArchitectureCQRS.Application.Queries;
using CleanArchitectureCQRS.Infrastructure.Persistence;
using MediatR;

namespace CleanArchitectureCQRS.Application.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IApplicationDbContext _context;

    public GetProductByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}
