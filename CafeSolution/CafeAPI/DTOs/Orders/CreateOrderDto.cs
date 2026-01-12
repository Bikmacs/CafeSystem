using CafeAPI.DTOs.OrderItems;

namespace CafeAPI.DTOs.Orders
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int? TableNumber { get; set; }
        public string Status { get; set; } = "Создан";
        public List<OrderItemCreateDto> Items { get; set; } = new List<OrderItemCreateDto>();
    }
}
