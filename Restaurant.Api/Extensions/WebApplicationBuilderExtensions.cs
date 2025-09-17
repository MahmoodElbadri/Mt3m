using Microsoft.OpenApi.Models;
using Restaurant.Api.Middlewares;
using Restaurant.Application.Extensions;
using Restaurant.Infrastructure.Extensions;
using Serilog;

namespace Restaurant.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentaionExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
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
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .WriteTo.Console();
        });
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<TimeLoggingMiddleware>();

    }
}
