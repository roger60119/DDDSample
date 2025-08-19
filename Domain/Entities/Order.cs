using System;
using System.Collections.Generic;

namespace DDDSample.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public string OrderNumber { get; private set; }
    public DateTime OrderDate { get; private set; }
    public int MemberId { get; private set; }

    public Order(string orderNumber, DateTime orderDate, int memberId)
    {
        OrderNumber = orderNumber;
        OrderDate = orderDate;
        MemberId = memberId;
    }

    public void Update(string orderNumber, DateTime orderDate, int memberId)
    {
        OrderNumber = orderNumber;
        OrderDate = orderDate;
        MemberId = memberId;
    }
}
