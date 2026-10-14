using CleanArchitectureCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureCQRS.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Price = 25000m },
            new Product { Id = 2, Name = "Mouse", Price = 500m },
            new Product { Id = 3, Name = "Keyboard", Price = 1500m }
        );
    }
}
