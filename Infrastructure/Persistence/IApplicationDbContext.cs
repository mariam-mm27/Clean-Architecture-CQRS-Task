using CleanArchitectureCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureCQRS.Infrastructure.Persistence;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
