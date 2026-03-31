using CafeAPI.Models.Helpers;

namespace CafeAPI.Models;

public class DishItems
{
    public int Id { get; set; }

    public int MenuItemId { get; set; }
    public  MenuItem? MenuItem { get; set; }

    public int IngredientId { get; set; }
    public  Indigriend? Ingredient { get; set; }

    public decimal Amount { get; set; }
    public UnitTypes UnitType { get; set; }
}