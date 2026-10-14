using CleanArchitectureCQRS.Application.DTOs;
using MediatR;

namespace CleanArchitectureCQRS.Application.Queries;

public class GetAllProductsQuery : IRequest<List<ProductDto>>
{
}
