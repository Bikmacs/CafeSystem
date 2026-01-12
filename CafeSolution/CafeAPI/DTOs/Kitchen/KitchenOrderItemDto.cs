namespace CafeAPI.DTOs.Kitchen
{
    public class KitchenOrderItemDto
    {
        public int OrderItemId { get; set; }
        public string Name { get; set; } = "";
        public int Quantity { get; set; }
    }
}
