namespace CafeAPI.DTOs.Products;

public class CreateProductDto
{
    public Models.Helpers.Products Product { get; set; }
    public decimal Quantity { get; set; }
    
}