using CafeAPI.Data;
using CafeAPI.DTOs.Category;
using CafeAPI.DTOs.MenuItems;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;
using CafeAPI.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static CafeAPI.Models.Helpers.ProductUnitHelper;

namespace CafeAPI.Services
{
    public class MenuService : IMenuService
    {
        private readonly DbContext _dbContext;
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
            ICategoryRepository categoryRepository,
            CafeDbContext dbContext)
        {
            _menuItemRepository = menuItemRepository;
            _orderItemRepository = orderItemRepository;
            _categoryRepository = categoryRepository;
            _cache = cache;
            _dbContext = dbContext;
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


        // public async Task<Dictionary<Products, UnitTypes>> AddNewProducts(Products productEnum, decimal quantityToAdd)
        // {
        //     try
        //     {
        //         var productUnits = ProductUnitHelper.GetProductUnits;
        //         string productName = productEnum.ToString();
        //
        //         var ingredient = await _dbContext.Ingredients
        //             .FirstOrDefaultAsync(i => i.Name == productName);
        //
        //         if (ingredient != null)
        //         {
        //             ingredient.Quantity += quantityToAdd;
        //             _dbContext.Ingredients.Update(ingredient);
        //         }
        //         else
        //         {
        //             var unit = productUnits.ContainsKey(productEnum)
        //                 ? productUnits[productEnum]
        //                 : UnitTypes.Kg; 
        //             
        //             var newIngredient = new Ingredient
        //             {
        //                 Name = productName,
        //                 Quantity = quantityToAdd,
        //                 UnitType = unit.ToString()
        //             };
        //
        //             await _dbContext.Ingredients.AddAsync(newIngredient);
        //         }
        //
        //         await _dbContext.SaveChangesAsync();
        //
        //         return new Dictionary<Products, UnitTypes>
        //         {
        //             { productEnum, productUnits[productEnum] }
        //         };
        //     }
        //     catch (Exception exception)
        //     {
        //         throw new Exception($"Ошибка при добавлении продукта '{productEnum}' на склад: {exception.Message}");
        //     }
        // }
    }
}