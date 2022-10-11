using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace WebHost.Tests.TestInfrastructure
{
    public static class TestHostCreator
    {
        public static TestServer CreateTestServer()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var webHost = new WebHostBuilder()
                .UseConfiguration(config)
                .UseStartup<Startup>();
            return new TestServer(webHost);
        }
    }
}
