using Skinet.Entities.Delivery;

namespace Skinet.Entities.OrderAggregate;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public required string BuyerEmail { get; set; }

    public ShippingAddress ShippingAddress { get; set; } = null!;

    public DeliveryMethod DeliveryMethod { get; set; } = null!;

    public PaymentSummary PaymentSummary { get; set; } = null!;

    public decimal Discount { get; set; }
    public string? CouponCode { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];

    public decimal SubTotal { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public required string PaymentIntentId { get; set; }

    public decimal GetTotal()
    {
        return SubTotal - Discount + DeliveryMethod.Price;
    }
}
