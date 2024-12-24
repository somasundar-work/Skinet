namespace Skinet.Entities.Product;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? PictureUrl { get; set; }
    public required string Brand { get; set; }
    public required string Category { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }
}
