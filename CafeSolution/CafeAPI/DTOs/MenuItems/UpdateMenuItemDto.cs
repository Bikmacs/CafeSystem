namespace CafeAPI.DTOs.MenuItems
{
    public class UpdateMenuItemDto
    {
        public int MenuItemId { get; set; }
        public string? Name { get; set; }     
        public string? Description { get; set; }
        public decimal? Price { get; set; }     
        public int? CategoryId { get; set; }
        public bool? Available { get; set; }  
        public string? Image { get; set; }
    }
}
