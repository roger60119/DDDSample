using DDDSample.Domain.Products.Entities;

namespace DDDSample.Domain.Orders.Entities;

public class Order
{
    public long OrderId { get; init; }
    public int MemberId { get; init; }
    public decimal OrderAmount { get; set; }
    public DateTime OrderDate { get; init; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
