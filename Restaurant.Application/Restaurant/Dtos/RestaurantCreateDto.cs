using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Restaurant.Dtos;

public class RestaurantCreateDto
{
    [Required, MaxLength(50), MinLength(3)]
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public string? Category { get; set; }
    public bool HasDelivery { get; set; }
    [Required, EmailAddress]
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    [RegularExpression(@"^\d[2]-\d[3]$",ErrorMessage = "Postal Code should be in format XX-XXX")]
    public string? PostalCode { get; set; }
}
