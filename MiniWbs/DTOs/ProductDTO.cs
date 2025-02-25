using System.ComponentModel.DataAnnotations;

namespace MiniWbs.DTOs;

public class ProductDTO
{
    [Required(ErrorMessage = "Productname måste anges")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Produktnamn måste vara mellan 1 och 100 tecken")]
    public required string ProductName { get; set; }

    [Required(ErrorMessage = "Priset måste anges")]
    [Range(1, 10000, ErrorMessage = "Priset måste anges och vara mellan 1 och 10000kr")]
    public int Price { get; set; }

    [Required(ErrorMessage = "Priset måste anges")]
    [Range(1, 10000, ErrorMessage = "Saldot måste anges och vara mellan 1 och 10000st")]
    public int Quantity { get; set; }
}
