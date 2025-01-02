using Microsoft.EntityFrameworkCore;
using HotelChatbot;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<HotelDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Configure MySQL with retry on failure (useful for transient connection issues)
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    );
});

// Add controllers (API endpoints)
builder.Services.AddControllers();

// Add Swagger services for API documentation (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Enables Swagger for API documentation

// Build the application
var app = builder.Build();

// Enable Swagger UI in the development environment
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Shows detailed errors in development
    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI(); // Enable Swagger UI for testing APIs
}

// Configure HTTP request pipeline
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS (if using HTTPS)
app.UseRouting(); // Use routing
app.UseAuthorization(); // Authorization middleware

// Map controllers to automatically pick up routes from your controller files
app.MapControllers();

// Render binds to the PORT environment variable, so let's use that for the port:
// Render uses the PORT environment variable to bind your app to the correct port
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000"; // Default to 5000 if PORT is not set

// Run the application on the specified port
app.Run($"http://0.0.0.0:{port}");
