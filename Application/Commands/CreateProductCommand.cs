using CleanArchitectureCQRS.Application.DTOs;
using MediatR;

namespace CleanArchitectureCQRS.Application.Commands;

public class CreateProductCommand : IRequest<ProductDto>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public CreateProductCommand(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
