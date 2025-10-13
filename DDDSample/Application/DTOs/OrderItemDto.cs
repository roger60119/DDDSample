namespace DDDSample.Application.DTOs;

public class OrderItemDto
{
    public int OrderItemId { get; set; }
    public long OrderId { get; set; }
    public int ProductId { get; set; }
    public ProductDto Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}