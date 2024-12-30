using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HotelChatbot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

            using (var scope = builder.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<HotelDbContext>();
                dbContext.Database.Migrate(); // Apply migrations on startup
            }

            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Adding appsettings.json configuration file
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // Configure the DbContext with MySQL connection string from configuration
                    services.AddDbContext<HotelDbContext>(options =>
                        options.UseMySql(
                            context.Configuration.GetConnectionString("DefaultConnection"),
                            ServerVersion.AutoDetect(context.Configuration.GetConnectionString("DefaultConnection"))
                        ));
                });
    }
}
