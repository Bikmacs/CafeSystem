using CafeAPI.DTOs.Category;
using CafeAPI.DTOs.MenuItems;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CafeAPI.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMemoryCache _cache;
        private const string MenuCacheKey = "menu_cache";
        private const string CategoryCacheKey = "category_cache";

        public MenuService(
            IMenuItemRepository menuItemRepository, 
            IMemoryCache cache,
            IOrderItemRepository orderItemRepository,
            ICategoryRepository categoryRepository)
        {
            _menuItemRepository = menuItemRepository;
            _orderItemRepository = orderItemRepository;
            _categoryRepository = categoryRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<MenuItemResponseDto>> GetMenuAsync()
        {
            if (_cache.TryGetValue(MenuCacheKey, out IEnumerable<MenuItemResponseDto>? menu))
            {
                if (menu != null) return menu;
            }

            var menuItems = await _menuItemRepository.GetAllMenuItemsAsync();
            /*
            if(menuItems == null) return Enumerable.Empty<MenuItemResponseDto>();
            */
            var result = menuItems.Select(FromEntity).ToList();

            _cache.Set(
                MenuCacheKey,
                result,
                TimeSpan.FromMinutes(1)
            );

            return result;
        }

        public async Task<MenuItemResponseDto> AddItemMenuAsync(CreateMenuItemDto createMenuItemDto)
        {
            var backItem = await _menuItemRepository.GetMenuItemByNameAsync(createMenuItemDto.Name);
            if (backItem != null) throw new InvalidOperationException("Блюдо уже существует");

            var menuItem = new MenuItem
            {
                Name = createMenuItemDto.Name,
                Description = createMenuItemDto.Description,
                Price = createMenuItemDto.Price,
                CategoryId = createMenuItemDto.CategoryId,
                Available = createMenuItemDto.Available
            };

            await _menuItemRepository.AddMenuItemAsync(menuItem);
            _cache.Remove(MenuCacheKey);

            return FromEntity(menuItem);
        }

        public async Task<UpdateMenuItemDto> UpdateItemMenu(int id, UpdateMenuItemDto updateMenuItemDto)
        {
            var backItem = await _menuItemRepository.GetMenuItemByIdAsync(id);
            if (backItem == null) return null!;

            backItem.Name = !string.IsNullOrEmpty(updateMenuItemDto.Name) ? updateMenuItemDto.Name : backItem.Name;
            backItem.Description = updateMenuItemDto.Description ?? backItem.Description;
            backItem.Price = updateMenuItemDto.Price ?? backItem.Price;
            backItem.Available = updateMenuItemDto.Available ?? backItem.Available;

            if (updateMenuItemDto.CategoryId.HasValue)
            {
                backItem.CategoryId = updateMenuItemDto.CategoryId.Value;
            }

            await _menuItemRepository.UpdateItemMenuAsync(id, backItem);

            _cache.Remove(MenuCacheKey);
            return updateMenuItemDto;
        }

        public async Task<bool> DeleteItemMenu(int id)
        {
            var backItem = await _menuItemRepository.GetMenuItemByIdAsync(id);
            if (backItem == null) return false;

            await _menuItemRepository.DeleteMenuItemAsync(backItem);
            _cache.Remove(MenuCacheKey);

            return true;
        }

        public Task<MenuItemResponseDto> GetMenuItemById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            if (_cache.TryGetValue(CategoryCacheKey, out List<CategoryDto>? categories))
            {
                if (categories != null) return categories;
            }

            var categoryEntities = await _categoryRepository.GetAllAsync();
            if (!categoryEntities.Any()) return new List<CategoryDto>();

            var result = categoryEntities.Select(c => new CategoryDto
            {
                Id = c.CategoryId,
                Name = c.Name
            }).ToList();

            _cache.Set(CategoryCacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }

        private static MenuItemResponseDto FromEntity(MenuItem item)
        {
            return new MenuItemResponseDto
            {
                MenuItemId = item.MenuItemId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Category = item.Category?.Name ?? "Категория не загружена",    
                Available = item.Available
            };
        }
    }
}