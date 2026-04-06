using CafeAPI.DTOs.MenuItems;
using CafeAPI.DTOs.Products;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task = DocumentFormat.OpenXml.Office2021.DocumentTasks.Task;

namespace CafeAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin,Waiter")]
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

        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file) // ← id, не idm
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран");
    
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);
    
            var fileName = $"{id}{Path.GetExtension(file.FileName)}"; 
            var filePath = Path.Combine(uploadFolder, fileName);      
    
            using (var fileStream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(fileStream);
    
            var updateDto = new UpdateMenuItemDto { Image = fileName };
            await menuService.UpdateItemMenu(id, updateDto);
    
            return Ok(new { ImageUrl = $"/images/{fileName}" });
        }
        
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await menuService.GetCategories();
            return result.Any() ? Ok(result) : NotFound("Список пуст");
        }
    }
}