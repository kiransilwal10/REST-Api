using GameStore.Api.Endpoints;
using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add logging configuration
builder.Logging.AddConsole();

// Get and validate connection string
var connString = builder.Configuration.GetConnectionString("GameStore");
if (string.IsNullOrEmpty(connString))
{
    throw new InvalidOperationException("Connection string 'GameStore' not found in configuration.");
}
Console.WriteLine($"Connection string: {connString}");

// Configure DbContext
builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseSqlite(connString)
        .LogTo(Console.WriteLine, LogLevel.Information));

var app = builder.Build();

// Database initialization
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.EnsureCreatedAsync();
        Console.WriteLine("Database created successfully at: " + connString);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error creating database: {ex.Message}");
    throw;
}

app.MapGamesEndpoints();

app.Run();