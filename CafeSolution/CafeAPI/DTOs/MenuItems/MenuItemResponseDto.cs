using CafeAPI.DTOs.MenuItems.Dish;

namespace CafeAPI.DTOs.MenuItems
{
    public class MenuItemResponseDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string Category { get; set; } = string.Empty;
        public bool Available { get; set; } = true;
        public string? Image { get; set; }

        public List<DishItemResponse> Ingredients { get; set; } = new();
    }
}
