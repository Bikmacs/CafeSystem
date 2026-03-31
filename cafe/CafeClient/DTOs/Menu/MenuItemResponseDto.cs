namespace CafeClient.DTOs.Menu
{
    public class MenuItemResponseDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool Available { get; set; }
        public string? Image { get; set; }

        public List<DishItemResponse> Ingredients { get; set; } = new();
    }
}