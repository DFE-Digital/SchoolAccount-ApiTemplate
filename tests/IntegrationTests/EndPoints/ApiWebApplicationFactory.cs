using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.EndPoints;

public class ApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
    }
}
