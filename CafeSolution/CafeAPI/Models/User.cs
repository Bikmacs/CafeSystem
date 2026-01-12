using System.ComponentModel.DataAnnotations;

namespace CafeAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)] 
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;


        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Role Role { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
