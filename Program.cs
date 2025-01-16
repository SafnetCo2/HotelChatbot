using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelChatbot; // Ensure the namespace is correct for your models

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<HotelDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Since you only have a production connection string (Render), we'll skip development-specific logic
    connectionString = "Server=mysql.render.com;Port=3306;Database=HotelDb;User=root;Password=Root%401234;";

    // Configure MySQL with retry on failure (useful for transient connection issues)
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
});

// Enable CORS to allow requests from any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add controllers (API endpoints)
builder.Services.AddControllers();

// Add Swagger services for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS for all endpoints
app.UseCors("AllowAll");

// Enable Swagger UI in the development environment
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure HTTP request pipeline
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Bind to a port (use environment variable PORT for Render)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
