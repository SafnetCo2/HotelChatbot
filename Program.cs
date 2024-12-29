using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HotelChatbot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Set up configuration sources.
                    config.SetBasePath(context.HostingEnvironment.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // Configure EF Core with MySQL
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<HotelDbContext>(options =>
                        options.UseMySql(
                            connectionString,
                            new MySqlServerVersion(new Version(8, 0, 26)) // Replace with your MySQL version
                        )
                    );

                    // Add other application services
                    services.AddSingleton<DatabaseContext>();

                    // Optional: Add logging services for debug or info output
                    services.AddLogging(config => config.AddConsole());
                });
    }
}