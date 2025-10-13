using DDDSample.Domain.Orders.Entities;
using DDDSample.Domain.Products.Entities;

namespace DDDSample.Domain.Orders.Entities;

public class OrderItem
{
    public int OrderItemId { get; init; }
    public long OrderId { get; init; }
    public int ProductId { get; init; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}