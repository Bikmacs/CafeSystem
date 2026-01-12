namespace CafeAPI.DTOs.MenuItems
{
    public class UpdateMenuItemDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal? Price { get; set; } = decimal.Zero;
        public int? CategoryId { get; set; }
        public bool? Available { get; set; } = true;
    }
}
