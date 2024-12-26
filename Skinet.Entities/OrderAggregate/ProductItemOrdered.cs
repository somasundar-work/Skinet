using System;

namespace Skinet.Entities.OrderAggregate;

public class ProductItemOrdered
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? PictureUrl { get; set; }
    public required string Brand { get; set; }
    public required string Category { get; set; }
}
