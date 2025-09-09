namespace DDDSample.Domain.Members.Entities;

public class Order
{
    public long OrderId { get; init; }
    public int OrderAmount { get; private set; }
    public DateTime OrderDate { get; private set; }
    public int MemberId { get; init; }

    public Order(int orderAmount, DateTime orderDate, int memberId)
    {
        OrderAmount = orderAmount;
        OrderDate = orderDate;
        MemberId = memberId;
    }
}
