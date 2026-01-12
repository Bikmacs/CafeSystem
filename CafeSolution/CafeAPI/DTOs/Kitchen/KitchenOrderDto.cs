namespace CafeAPI.DTOs.Kitchen
{
    public class KitchenOrderDto
    {
        public int OrderId { get; set; }
        public int TableNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "";
        public List<KitchenOrderItemDto> Items { get; set; } = new();
    }
}
