using CafeAPI.DTOs.MenuItems;
using CafeAPI.Models;
using IntegrationTests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Controllers
{
    [TestFixture]
    internal class MenuControllerTests : BaseIntegrationTest
    {
        [Test]
        public async Task GetMenuAsync_ReturnMenu()
        {
            var testCategory = new Category { Name = "Чай" };
            Dbcontext.Category.Add(testCategory);

            var testMenuItem = new MenuItem
            {
                Name = "Черный чай",
                Price = 150,
                Description = "Авторский чай",
                Available = true,
                Category = testCategory
            };

            Dbcontext.MenuItems.Add(testMenuItem);
            await Dbcontext.SaveChangesAsync();

            var response = await HttpClient.GetAsync("/api/Menu/GetMenu");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var menuItems = await response.Content.ReadFromJsonAsync<MenuItemResponseDto[]>();

            Assert.IsNotNull(menuItems, "Ответ от API не должен быть пустым");
            Assert.AreEqual(1, menuItems.Length, "В списке должен быть ровно 1 предмет");
            Assert.AreEqual("Черный чай", menuItems[0].Name);
            Assert.IsTrue(menuItems[0].Available, "Поле должно быть true");
        }
    }
}
         
    

