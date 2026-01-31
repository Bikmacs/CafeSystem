using CafeAPI.DTOs.Category;
using CafeAPI.DTOs.MenuItems;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;
using CafeAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace CafeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("GetMenu")]
        public async Task<IActionResult> GetMenu()
        {
            var result = await _menuService.GetMenuAsync();
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddItem([FromBody] CreateMenuItemDto dto)
        {
            try
            {
                var result = await _menuService.AddItemMenuAsync(dto);
                return CreatedAtAction(nameof(GetMenuItemById), new { id = result.MenuItemId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var success = await _menuService.DeleteItemMenu(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var item = await _menuService.GetMenuItemById(id);
            return item == null ? NotFound() : Ok(item);
        }
    }

}

