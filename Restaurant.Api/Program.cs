using Restaurant.Api.Extensions;
using Restaurant.Api.Middlewares;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Seeders;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.AddPresentaionExtensions();
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>(); //we put it in the first line so that it can catch all the exceptions and also log them
app.UseMiddleware<TimeLoggingMiddleware>(); //we put it in the first line so that it can log the exact time time taken by each request 
app.UseSerilogRequestLogging();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();
app.MapGroup("api/identity").MapIdentityApi<User>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



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
    throw;
}

app.Run();
