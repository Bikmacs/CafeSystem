using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeClient.DTOs.Orders
{
    public class BillDto
    {
        public int Id { get; set; }
        public ObservableCollection<OrderItemDto> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);
    }
}
