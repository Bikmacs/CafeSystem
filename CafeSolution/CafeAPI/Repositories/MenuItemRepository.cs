using CafeAPI.Data;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Repositories
{
    public class MenuItemRepository(CafeDbContext context) : IMenuItemRepository
    {
        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            context.MenuItems.Add(menuItem);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(MenuItem menuItem)
        {
            context.MenuItems.Remove(menuItem);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await context.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Tags) 
                .Include(m => m.DishItems)
                .ThenInclude(di => di.Ingredient)
                .ToListAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemByCategoryAsync(string category)
        {
            return await context.MenuItems
                .Include(mi => mi.Category)
                .Include(mi => mi.Tags) 
                .Where(mi => mi.Category.Name == category)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await context.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Tags) 
                .Include(m => m.DishItems)           
                .ThenInclude(di => di.Ingredient)     
                .FirstOrDefaultAsync(m => m.MenuItemId == id);
        }

        public async Task<MenuItem?> GetMenuItemByNameAsync(string name)
        {
            return await context.MenuItems
                .Include(m => m.Tags) 
                .FirstOrDefaultAsync(item => item.Name == name);
        }
        
        public async Task UpdateItemMenuAsync(int id, MenuItem menuItem)
        {
            var itemMenu = await context.MenuItems
                .Include(m => m.Tags)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);

            if (itemMenu != null)
            {
                itemMenu.Name = menuItem.Name;
                itemMenu.Description = menuItem.Description;
                itemMenu.Price = menuItem.Price;
                itemMenu.CategoryId = menuItem.CategoryId; 
                itemMenu.Available = menuItem.Available;

                if (menuItem.Tags != null)
                {
                    itemMenu.Tags = menuItem.Tags;
                }
                
                await context.SaveChangesAsync();
            }
        }
    }
}