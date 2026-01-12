using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeClient.DTOs.Orders
{
    public class TableDto
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public string DisplayName { get; set; }
        public bool IsBusy { get; set; } 
    }
}