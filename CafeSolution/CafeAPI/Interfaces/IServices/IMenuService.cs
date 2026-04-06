using CafeAPI.DTOs.Category;
using CafeAPI.DTOs.MenuItems;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.Interfaces.IServices
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItemResponseDto>> GetMenuAsync();
        Task<MenuItemResponseDto> AddItemMenuAsync(CreateMenuItemDto createMenuItemDto);
        Task<UpdateMenuItemDto> UpdateItemMenu(int id, UpdateMenuItemDto updateMenuItemDto);
        Task<bool> DeleteItemMenu(int id);
        Task<MenuItemResponseDto> GetMenuItemById(int id);
        Task<List<CategoryDto>> GetAll();
        Task<List<CategoryDto>> GetCategories();


    }
}
