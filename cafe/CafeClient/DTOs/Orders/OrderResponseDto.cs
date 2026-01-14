using CafeClient.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        public ObservableCollection<OrderItemDto> Items { get; set; } = new();
        public ObservableCollection<BillDto> Bills { get; set; } = new();
    }
}