using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;


namespace IntegrationTests.Infrastructure
{
    public class CafeApiFactory : WebApplicationFactory<Program>
    {
        public CafeApiFactory()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
        }
    }
}