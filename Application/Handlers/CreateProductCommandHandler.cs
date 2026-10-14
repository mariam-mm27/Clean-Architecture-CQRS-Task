using CleanArchitectureCQRS.Application.Commands;
using CleanArchitectureCQRS.Application.DTOs;
using CleanArchitectureCQRS.Domain.Entities;
using CleanArchitectureCQRS.Infrastructure.Persistence;
using MediatR;

namespace CleanArchitectureCQRS.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}
