using CafeAPI.DTOs.OrderItems;

namespace CafeAPI.DTOs.Orders
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int? TableNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Создан";
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
