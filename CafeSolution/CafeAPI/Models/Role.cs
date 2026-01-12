using System.ComponentModel.DataAnnotations;

namespace CafeAPI.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
