using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeAPI.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; init; }

        [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

        [MaxLength(500)] public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; } = decimal.Zero;
        public int CategoryId { get; set; }
        public bool Available { get; set; } = true;

        [MaxLength(255)] public string? Image { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<DishItems> DishItems { get; set; } = new List<DishItems>();

        public ICollection<OrderItem> OrderItems { get; init; } = new List<OrderItem>();
    }
}