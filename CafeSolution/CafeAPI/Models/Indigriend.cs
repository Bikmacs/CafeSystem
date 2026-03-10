namespace CafeAPI.Models;

public class Indigriend
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<DishItems> DishItems { get; set; } = new List<DishItems>();
}