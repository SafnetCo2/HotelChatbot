using Microsoft.EntityFrameworkCore;
using HotelChatbot;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))
    
    )
);

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

// Run the application
app.Run();
