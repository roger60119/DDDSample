namespace DDDSample.Domain.Products.Entities;

public class Product
{
    public int ProductId { get; init; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public int Stock { get; set; }
}