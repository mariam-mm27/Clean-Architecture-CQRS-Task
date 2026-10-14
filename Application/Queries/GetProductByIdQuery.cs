using CleanArchitectureCQRS.Application.DTOs;
using MediatR;

namespace CleanArchitectureCQRS.Application.Queries;

public class GetProductByIdQuery : IRequest<ProductDto?>
{
    public int Id { get; set; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}
