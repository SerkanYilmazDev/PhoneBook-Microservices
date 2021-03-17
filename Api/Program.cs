using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Shared.Logging;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .UseLogging("Api-Gateway")
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("ocelot.json")
                            .AddEnvironmentVariables();
                   });
    }
}
