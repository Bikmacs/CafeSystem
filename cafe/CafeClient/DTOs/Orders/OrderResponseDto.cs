using CafeClient.DTOs.Orders;
using System;
using System.Collections.Generic;

namespace CafeClient.DTOs
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public string? UserName { get; set; } 
        public int UserId { get; set; }
        public int? TableNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}