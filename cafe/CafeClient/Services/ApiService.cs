using CafeClient.DTOs;
using CafeClient.DTOs.Category;
using CafeClient.DTOs.Menu;
using CafeClient.DTOs.Orders;
using CafeClient.DTOs.User;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CafeClient.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService()
        {
            var baseUrl = App.Configuration["Api:BaseUrl"] 
                ?? throw new InvalidOperationException("Api:BaseUrl not configured");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        
        public async Task<LoginResponseDto?> LoginAsync(string login, string password)
        {
            // Создаем DTO для передачи данных
            var loginDto = new LoginUserDto
            {
                Login = login,
                Password = password
            };
            // Выполняем POST-запрос
            try
            {
                // Добавляем логирование запроса
                LogRequest("api/User/Login");
                // Отправляем запрос на сервер 
                var response = await _httpClient.PostAsJsonAsync("api/User/Login", loginDto );
                if (response.IsSuccessStatusCode) // Проверяем успешность ответа
                {
                    // Читаем и возвращаем данные из ответа
                    return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                }
                else return null;
            }
            //возможные исключения
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<MenuItemResponseDto>?> GetMenuAsync()
        {
            try
            {
                LogRequest("api/Menu/GetMenu");
                var response = await _httpClient.GetAsync("api/Menu/GetMenu");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MenuItemResponseDto>>();
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<OrderResponseDto>?> GetAllOrdersAsync()
        {
            try
            {
                LogRequest("api/Orders/GetAll");
                return await _httpClient.GetFromJsonAsync<List<OrderResponseDto>>("api/Orders/GetAll");
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"ОШИБКА ЗАГРУЗКИ ЗАКАЗОВ: {ex.Message}"); 
                return null;
            }
        }

        public async Task<OrderResponseDto?> GetOrdersByIdAsync(int id)
        {
            try
            {
                LogRequest($"/api/Orders/{id}/GetOrderById");
                return await _httpClient.GetFromJsonAsync<OrderResponseDto>($"/api/Orders/{id}/GetOrderById");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ОШИБКА ЗАГРУЗКИ ЗАКАЗОВ ПО СТОЛУ: {ex.Message}");
                return null;
            }
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            try
            {
                LogRequest("api/User");
                return await _httpClient.GetFromJsonAsync<List<UserResponseDto>>("api/User");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ОШИБКА ЗАГРУЗКИ ЗАКАЗОВ: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> RegisterUserAsync(CreateUserDto userDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/User/Register", userDto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ОШИБКА РЕГИСТРАЦИИ: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/User/DeleteUser?id={userId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ОШИБКА УДАЛЕНИЯ: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(int userId, CreateUserDto userDto, int roleID)
        {
            try
            {
                string url = $"api/User/{userId}/UpdateUser?Role={roleID}";
                var response = await _httpClient.PatchAsJsonAsync(url, userDto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ОШИБКА ОБНОВЛЕНИЯ: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            try
            {
                string url = $"api/Orders/{orderId}/statusUpdate";
                var response = await _httpClient.PatchAsJsonAsync(url, newStatus);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ОШИБКА СМЕНЫ СТАТУСА: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteOrderItemAsync(int orderId, int orderItemId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Orders/{orderId}/deleteItem?orderItemId={orderItemId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ОШИБКА УДАЛЕНИЯ: {ex.Message}");
                return false;
            }
        }

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Orders/CreateOrder", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderResponseDto>();
        }

        public async Task<bool> AddItemsToOrderAsync(int orderId, List<CreateOrderItemDto> newItems)
        {
            try
            {
                var dto = new CreateOrderDto
                {
                    UserId = CurrentUser.UserId,
                    TableNumber = 0,
                    Status = "Update",
                    Items = newItems
                };

                var response = await _httpClient.PostAsJsonAsync($"api/Orders/{orderId}/AddItemsToOrder", dto);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка добавления блюд: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Orders/{orderId}/DeleteOrder");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ОШИБКА УДАЛЕНИЯ ЗАКАЗА: {ex.Message}");
                return false;
            }
        }

        public async Task<List<OrderResponseDto>?> GetKitchenOrdersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<OrderResponseDto>>("api/Orders/GetActiveOrders");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Kitchen Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddMenuAsync(CreateMenuItemDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Menu/Add", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ОШИБКА ДОБАВЛЕНИЯ БЛЮДА: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Menu/{id}/DeleteItem");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ОШИБКА УДАЛЕНИЯ БЛЮДА: {ex.Message}");
                return false;
            }
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryDto>>("Category");
        }


        public void Logout()
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
        }

        public void SetAuthorizationToken(string token)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            Debug.WriteLine("Authorization header set: " + _httpClient.DefaultRequestHeaders.Authorization);
        }

        private void LogRequest(string endpoint)
        {
            Debug.WriteLine("---- HTTP REQUEST ----");
            Debug.WriteLine($"Endpoint: {endpoint}");
            Debug.WriteLine($"Authorization: {_httpClient.DefaultRequestHeaders.Authorization}");
            Debug.WriteLine("----------------------");
        }

    }
}
