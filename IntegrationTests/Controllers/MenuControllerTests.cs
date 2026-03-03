using CafeAPI.DTOs.MenuItems;
using CafeAPI.Models;
using IntegrationTests.Infrastructure;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.Controllers
{
    [TestFixture]
    internal class MenuControllerTests : BaseIntegrationTest
    {
        private MenuItem _testMenuItem;
        private int _rnd = new Random().Next(1, 31);

        [SetUp]
        public void MenuItemSetUp()
        {
            
            
            var testCategory = new Category { Name = "Чай" };
            _testMenuItem = new MenuItem
            {
                Name = "Черный чай",
                Price = 150,
                Description = "Авторский чай",
                Available = true,
                Category = testCategory
            };
        }

        [Test]
        public async Task GetMenuAsync_ReturnMenu()
        {
            Dbcontext.MenuItems.Add(_testMenuItem);
            await Dbcontext.SaveChangesAsync();

            var response = await HttpClient.GetAsync("/api/Menu/GetMenu");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var menuItems = await response.Content.ReadFromJsonAsync<MenuItemResponseDto[]>();
            if (menuItems != null)
            {
                var tea = menuItems.FirstOrDefault(t => t.Name == "Черный чай");
                Assert.IsNotNull(menuItems, "Ответ от API не должен быть пустым");
                Assert.IsNotNull(tea, "Добавленный чай не найден в списке меню!");
                Assert.IsTrue(tea.Available, "Поле должно быть true");
            }
        }

        [Test]
        public async Task AddEatOnMenu()
        {
            Dbcontext.Category.Add(_testMenuItem.Category);
            await Dbcontext.SaveChangesAsync();

            var testMenuItem = new CreateMenuItemDto
            {
                Name = "Чизкейк",
                Description = "Вкусный чизкейк",
                Price = 350,
                CategoryId = _testMenuItem.Category.CategoryId,
                Available = true,
            };

            var response = await HttpClient.PostAsJsonAsync("/api/Menu/Add", testMenuItem);
            Assert.IsTrue(response.IsSuccessStatusCode, $"Сервер вернул: {response.StatusCode}");

            var itemDb = Dbcontext.MenuItems.FirstOrDefault(t => t.Name == "Чизкейк");

            Assert.IsNotNull(itemDb, "Блюдо не было сохранено в базу данных!");
            Assert.AreEqual(350, itemDb.Price, "Цена сохранилась неправильно!");
        }

        [Test]
        public async Task DeleteMenuAsync_ReturnMenu()
        {
            Dbcontext.MenuItems.Add(_testMenuItem);
            await Dbcontext.SaveChangesAsync();

            var id = _testMenuItem.MenuItemId;

            var response = await HttpClient.DeleteAsync($"/api/Menu/{id}");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            Dbcontext.ChangeTracker.Clear();

            var deleteItem = Dbcontext.MenuItems
                .FirstOrDefault(d => d.MenuItemId == id);
            Assert.That(deleteItem, Is.Null);
        }

        [Test]
        public async Task GetMenuItemById()
        {
            Dbcontext.ChangeTracker.Clear();
            
            var response = await HttpClient.DeleteAsync($"/api/Menu/{_rnd}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"_rnd: {_rnd}");
        }
        
    }
}