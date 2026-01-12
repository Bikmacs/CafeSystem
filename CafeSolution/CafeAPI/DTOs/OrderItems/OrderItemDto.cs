using CafeAPI.DTOs.Orders;
using CafeAPI.Models;

namespace CafeAPI.DTOs.OrderItems
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
