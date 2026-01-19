using CafeAPI.Models;

namespace CafeAPI.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
    }
}
