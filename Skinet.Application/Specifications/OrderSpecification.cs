using System;
using Skinet.Entities.OrderAggregate;

namespace Skinet.Application.Specifications;

public class OrderSpecification : BaseSpecification<Order>
{
    public OrderSpecification(string email)
        : base(x => x.BuyerEmail == email)
    {
        AddInclude(x => x.OrderItems);
        AddInclude(x => x.DeliveryMethod);
        AddOrderByDescending(x => x.OrderDate);
    }

    public OrderSpecification(string email, int id)
        : base(x => x.BuyerEmail == email && x.Id == id)
    {
        AddInclude("OrderItems");
        AddInclude("DeliveryMethod");
    }

    public OrderSpecification(string paymentIntentId, bool isPaymentIntent = true)
        : base(x => x.PaymentIntentId == paymentIntentId)
    {
        AddInclude("OrderItems");
        AddInclude("DeliveryMethod");
    }
}
