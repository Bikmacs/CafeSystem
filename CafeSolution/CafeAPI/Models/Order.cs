using System.ComponentModel.DataAnnotations;

namespace CafeAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int? TableNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Создан";
       
        public User? User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }

    }
}
