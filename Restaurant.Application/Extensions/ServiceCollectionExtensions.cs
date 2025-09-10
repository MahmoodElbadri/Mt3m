using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurants;

namespace Restaurant.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();
    }
}
