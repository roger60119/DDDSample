namespace DDDSample.Application.DTOs;

public class ProductDto
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public int Stock { get; set; }
}