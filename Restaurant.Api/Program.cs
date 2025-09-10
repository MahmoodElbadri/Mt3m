using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;
using Restaurant.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Better seeding approach with proper error handling
try
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

    Console.WriteLine("Starting database seeding...");
    await seeder.SeedData();
    Console.WriteLine("Database seeding completed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error during seeding: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    // Optionally, you can choose to throw or continue based on your needs
    throw; // This will prevent the app from starting if seeding fails
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.Use(async (context, next) =>
    {
        context.Response.Redirect("/swagger/index.html");
        await next();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();