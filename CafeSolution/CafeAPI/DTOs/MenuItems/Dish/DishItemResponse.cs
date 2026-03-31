namespace CafeAPI.DTOs.MenuItems.Dish;

public class DishItemResponse
{
    public string IngredientName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Unit { get; set; } = string.Empty;
}