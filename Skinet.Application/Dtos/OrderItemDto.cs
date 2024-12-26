using System;

namespace Skinet.Application.Dtos;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? PictureUrl { get; set; }
    public required string Brand { get; set; }
    public required string Category { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
}
