using CafeAPI.Data;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly CafeDbContext _context;

        public MenuItemRepository(CafeDbContext context)
        {
            _context = context;
        }
        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItems
                .Include(m => m.Category)
                .ToListAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemByCategoryAsync(string category)
        {
            return await _context.MenuItems
                .Include(mi => mi.Category)
                .Where(mi => mi.Category.Name == category)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await _context.MenuItems.
                Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);
        }

        public async Task<MenuItem?> GetMenuItemByNameAsync(string name)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(item => item.Name == name);
        }
        
        public async Task UpdateItemMenuAsync(int id, MenuItem menuItem)
        {
            var itemMenu = await _context.MenuItems.FindAsync(id);
            if (itemMenu != null)
            {
                itemMenu.Name = menuItem.Name;
                itemMenu.Description = menuItem.Description;
                itemMenu.Price = menuItem.Price;
                itemMenu.Category = menuItem.Category;
                itemMenu.Available = menuItem.Available;
            }
            await _context.SaveChangesAsync();
        }
    }
}
