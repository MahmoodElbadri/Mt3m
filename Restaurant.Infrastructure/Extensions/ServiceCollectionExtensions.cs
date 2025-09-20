using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Infrastructure.Seeders;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Repository;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Authorization;

namespace Restaurant.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<Persistence.RestaurantDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddIdentityApiEndpoints<User>()
            .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RestaurantDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
    }
}
