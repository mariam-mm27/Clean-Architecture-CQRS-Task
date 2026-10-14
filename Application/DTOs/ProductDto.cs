namespace CleanArchitectureCQRS.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
