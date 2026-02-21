using CafeAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Infrastructure
{
    public abstract class BaseIntegrationTest
    {
        protected CafeApiFactory Factory;
        protected HttpClient HttpClient;
        protected CafeDbContext Dbcontext;

        [SetUp]
        public void BaseSetup()
        {   
            //Создание сервера
            Factory = new CafeApiFactory();
            HttpClient = Factory.CreateClient();
            //Создание бд
            var scope = Factory.Services.CreateScope();
            Dbcontext = scope.ServiceProvider.GetRequiredService<CafeDbContext>();

            //Отчистка 
            Dbcontext.Database.EnsureDeleted();
            Dbcontext.Database.EnsureCreated();

        }

        [TearDown]
        public void BaseTearDown()
        {
            HttpClient.Dispose();
            Factory.Dispose();
            Dbcontext.Dispose();
        }
    }
}
