using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeClient.DTOs.Orders
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int TableNumber { get; set; } // Сервер ждет именно TableNumber
        public string Status { get; set; } = "Открыт"; // Значение по умолчанию

        public List<CreateOrderItemDto> Items { get; set; } = new();
    }

}
