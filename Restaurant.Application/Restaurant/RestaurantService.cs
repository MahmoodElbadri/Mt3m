
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants;

public class RestaurantService(IRestaurantRepository _restRepo, ILogger<RestaurantService> _logger) : IRestaurantService
{
    public async Task<Domain.Entities.Restaurant?> GetRestaurantById(int id)
    {
        _logger.LogInformation($"Getting Restaurant by {id}");
        var rest = await _restRepo.GetRestaurantById(id);
        return rest;
    }

    public Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurants()
    {
        _logger.LogInformation("Getting all Restaurants");
        var restaurants = _restRepo.GetRestaurants();
        return restaurants;
    }
}
