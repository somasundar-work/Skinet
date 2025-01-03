using System;
using Skinet.Entities.OrderAggregate;

namespace Skinet.Entities.Cart;

public class ShoppingCart
{
    public required string Id { get; set; }
    public List<CartItem> Items { get; set; } = [];
    public int? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? Currency { get; set; }
    public AppCoupon? Coupon { get; set; }
}
