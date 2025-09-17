using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;
using Restaurant.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Restaurant.Api.Middlewares;
using Restaurant.Domain.Entities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(tmp =>
{
    tmp.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
    });
    tmp.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BearerAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Host.UseSerilog((ctx, configureLogger) =>
{
    configureLogger
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console();
});
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<TimeLoggingMiddleware>();

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