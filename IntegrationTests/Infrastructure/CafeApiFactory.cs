using CafeAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Infrastructure
{
    public class CafeApiFactory: WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(service =>
            {
                var descriptor = service.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<CafeDbContext>));

                if (descriptor != null) service.Remove(descriptor);

                service.AddDbContext<CafeDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryCafeTestDb");
                });
            });
            builder.UseEnvironment("Testing");
        }
    }
}
