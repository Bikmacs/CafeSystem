using CafeAPI.DTOs.Category;
using CafeAPI.DTOs.MenuItems;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Models;
using CafeAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace CafeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MenuController(IMenuItemRepository menuItemRepository, ICategoryRepository categoryRepository)
        {
            _menuItemRepository = menuItemRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("GetMenu")]
        public async Task<ActionResult<IEnumerable<MenuItemResponseDto>>> GetMenuAsync()
        {
            var menuItems = await _menuItemRepository.GetAllMenuItemsAsync();

            var response = menuItems.Select(item => new MenuItemResponseDto
            {
                MenuItemId = item.MenuItemId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Category = item.Category != null ? item.Category.Name : "Без категории",
                Available = item.Available
            });

            return Ok(response);
        }

    
        
        [HttpPost("Add")]
        public async Task<ActionResult<MenuItemResponseDto>> AddItemMenu([FromBody] CreateMenuItemDto createMenuItemDto)
        {
            var existingItem = await _menuItemRepository.GetMenuItemByNameAsync(createMenuItemDto.Name);
            if(existingItem != null)
            {
                ModelState.AddModelError("Name", "Блюдо с таким названием уже существует.");
                return BadRequest(ModelState);
            }

            var newItem = new MenuItem
            {
                Name = createMenuItemDto.Name,
                Description = createMenuItemDto.Description,
                Price = createMenuItemDto.Price,
                CategoryId = createMenuItemDto.CategoryId,
                Available = createMenuItemDto.Available
            };

            await _menuItemRepository.AddMenuItemAsync(newItem);
            var fullItem = await _menuItemRepository.GetMenuItemByIdAsync(newItem.MenuItemId);

            if (fullItem == null) return BadRequest();

            var responseDto = new MenuItemResponseDto
            {
                MenuItemId = fullItem.MenuItemId,
                Name = fullItem.Name,
                Description = fullItem.Description,
                Price = fullItem.Price,
                Category = fullItem.Category != null ? fullItem.Category.Name : "Без категории",
                Available = fullItem.Available
            };
            return CreatedAtAction(nameof(GetMenuItemById), new { id = fullItem.MenuItemId }, responseDto);
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateMenuItemDto>> UpdateItemMenu(int id, [FromBody] UpdateMenuItemDto updateMenuItemDto)
        {

            if (id != updateMenuItemDto.MenuItemId)
            {
                return BadRequest("ID в URL не совпадает с ID в теле запроса.");
            }

            var existingItem = await _menuItemRepository.GetMenuItemByIdAsync(updateMenuItemDto.MenuItemId);
            if(existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateMenuItemDto.Name;
            existingItem.Description = updateMenuItemDto.Description ?? existingItem.Description;
            existingItem.Price = updateMenuItemDto.Price ?? existingItem.Price;
            existingItem.Available = updateMenuItemDto.Available ?? existingItem.Available;

            if (updateMenuItemDto.CategoryId.HasValue)
            {
                existingItem.CategoryId = updateMenuItemDto.CategoryId.Value;
            }

            await _menuItemRepository.UpdateItemMenuAsync(existingItem.MenuItemId, existingItem);
            return Ok(updateMenuItemDto);

        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}/DeleteItem")]
        public async Task<IActionResult> DeleteItemMenu(int id)
        {
            var existingItem = await _menuItemRepository.GetMenuItemByIdAsync(id);
            if(id == 0 || existingItem == null)
            {
                return NotFound();
            }
            await _menuItemRepository.DeleteMenuItemAsync(existingItem);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}/MenuItemId")]
        public async Task<ActionResult<MenuItemResponseDto>> GetMenuItemById(int id)
        {
            var item = await _menuItemRepository.GetMenuItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            var responseDto = new MenuItemResponseDto
            {
                MenuItemId = item.MenuItemId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Category = item.Category != null ? item.Category.Name : "Без категории",
                Available = item.Available
            };

            return Ok(responseDto);
        }

        [HttpGet("/Category")]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var result = categories.Select(c => new CategoryDto
            {
                Id = c.CategoryId,
                Name = c.Name
            });

            return Ok(result);
        }

    }
}
