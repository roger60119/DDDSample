namespace DDDSample.Application.DTOs;

public class OrderDto
{
    public long OrderId { get; private set; }
    public int OrderAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public int MemberId { get; set; }
}