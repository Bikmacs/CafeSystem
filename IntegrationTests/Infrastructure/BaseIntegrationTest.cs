using CafeAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;

namespace IntegrationTests.Infrastructure
{
    public abstract class BaseIntegrationTest
    {
        private CafeApiFactory _factory;
        protected HttpClient HttpClient;
        protected CafeDbContext Dbcontext;

        [SetUp]
        public void BaseSetup()
        {
            //Создание сервера
            _factory = new CafeApiFactory();
            HttpClient = _factory.CreateClient();
            //Создание бд
            var scope = _factory.Services.CreateScope();
            Dbcontext = scope.ServiceProvider.GetRequiredService<CafeDbContext>();
            //Отчистка 
            Dbcontext.Database.EnsureDeleted();
            Dbcontext.Database.EnsureCreated();
        }

        private string GetJwtTokenForRole(int roleId)
        {
            var scope = _factory.Services.CreateScope();
            var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();

            var testUser = new User
            {
                UserId = 1,
                Login = "Администратор",
                RoleId = roleId
            };
            return tokenService.CreateToken(testUser);
        }

        private void AuthenticateClientAsRole(int roleId)
        {
            var token = GetJwtTokenForRole(roleId);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        protected void AuthenticateAdminAsRole() => AuthenticateClientAsRole(1);
        protected void AuthenticateWaiterAsRole() => AuthenticateClientAsRole(2);
        protected void AuthenticateCookAsRole() => AuthenticateClientAsRole(3);
        

        [TearDown]
        public void BaseTearDown()
        {
            HttpClient.Dispose();
            _factory.Dispose();
            Dbcontext.Dispose();
        }
    }
}