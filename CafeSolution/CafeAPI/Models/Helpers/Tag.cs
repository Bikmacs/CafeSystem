using System.ComponentModel.DataAnnotations;

namespace CafeAPI.Models.Helpers;

public class Tag
{
    public int TagId { get; set; } 
    
    [Required]
    public string TagName { get; set; } = string.Empty;
    
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}