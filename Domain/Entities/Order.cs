namespace DDDSample.Domain.Entities;

public class Order
{
    public long OrderId { get; private set; }
    public int OrderAmount { get; private set; }
    public DateTime OrderDate { get; private set; }
    public int MemberId { get; private set; }

    public Order(int orderAmount, DateTime orderDate, int memberId)
    {
        OrderAmount = orderAmount;
        OrderDate = orderDate;
        MemberId = memberId;
    }

    public void Update(int orderAmount, DateTime orderDate, int memberId)
    {
        OrderAmount = orderAmount;
        OrderDate = orderDate;
        MemberId = memberId;
    }
}
