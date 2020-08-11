using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Publicator.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureLogging((webHostBuilder ,logBuilder) =>
                    {
                        logBuilder.ClearProviders();
                        logBuilder.AddConsole();
                        logBuilder.AddConfiguration(webHostBuilder.Configuration);
                    });
                });
    }
}
