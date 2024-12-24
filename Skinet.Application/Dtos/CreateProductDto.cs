using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Skinet.Application.Dtos;

public class CreateProductDto
{
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public string? PictureUrl { get; set; }

    [Required]
    public string Brand { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public decimal QuantityInStock { get; set; }

    [DefaultValue(true)]
    public bool? IsActive { get; set; }
}
