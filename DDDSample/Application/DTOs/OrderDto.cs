using DDDSample.Domain.Products.Entities;

namespace DDDSample.Application.DTOs;

public class OrderDto
{
    public long OrderId { get; set; }
    public int MemberId { get; set; }
    public decimal OrderAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}