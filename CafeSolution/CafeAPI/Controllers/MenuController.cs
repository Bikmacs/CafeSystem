using CafeAPI.DTOs.MenuItems;
using CafeAPI.DTOs.Products;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Waiter")]
    [ApiController]
    public class MenuController(IMenuService menuService) : ControllerBase
    {
        [HttpGet("GetMenu")]
        public async Task<IActionResult> GetMenu()
        {
            var result = await menuService.GetMenuAsync();
            return result.Any() ? Ok(result) : NotFound("Список пуст");
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddItem([FromBody] CreateMenuItemDto dto)
        {
            try
            {
                var result = await menuService.AddItemMenuAsync(dto);
                return CreatedAtAction(nameof(GetMenuItemById), new { id = result.MenuItemId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var success = await menuService.DeleteItemMenu(id);
            return success ? Ok("удалено") : NotFound("Не найдено");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var item = await menuService.GetMenuItemById(id);
            return Ok(item);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> AddProducts([FromQuery] CreateProductDto productDto)
        {
            try
            {
                var unit = ProductUnitHelper.GetProductUnits[productDto.Product];
                
                
                return Ok(new { 
                    Message = $"Запасы пополнены: {productDto.Product} — {productDto.Quantity} {unit}." 
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}