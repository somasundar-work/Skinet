using System;

namespace Skinet.Entities.Cart;

public class CartItem
{
    public required int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required decimal Price { get; set; }
    public required decimal Quantity { get; set; }
    public required string PictureUrl { get; set; }
    public required string Brand { get; set; }
    public required string Category { get; set; }
}
