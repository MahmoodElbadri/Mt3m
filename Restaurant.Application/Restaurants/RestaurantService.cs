
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants;

public class RestaurantService(IRestaurantRepository _restRepo, ILogger<RestaurantService> _logger) : IRestaurantService
{
    public Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurants()
    {
        _logger.LogInformation("Getting all Restaurants");
        var restaurants = _restRepo.GetRestaurants();
        return restaurants;
    }
}
