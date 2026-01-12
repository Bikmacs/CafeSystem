using CafeAPI.Models;

namespace CafeAPI.Interfaces.IRepository
{
    public interface IMenuItemRepository
    {
        Task AddMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(MenuItem menuItem);
        Task UpdateItemMenuAsync(int id,MenuItem menuItem);
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
        Task<MenuItem?> GetMenuItemByNameAsync(string name);
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<List<MenuItem>> GetMenuItemByCategoryAsync(string category);


    }
}
