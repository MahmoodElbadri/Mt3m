
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.Dtos;
using Restaurant.Application.Restaurant.Dtos;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants;

public class RestaurantService(IRestaurantRepository _restRepo, ILogger<RestaurantService> _logger) : IRestaurantService
{
    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
    {
        _logger.LogInformation($"Getting Restaurant by {id}");
        var rest = await _restRepo.GetRestaurantByIdAsync(id);
        var restaurantDto = RestaurantDto.FromEntity(rest);
        return restaurantDto;
    }

    public async Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync()
    {
        _logger.LogInformation("Getting all Restaurants");
        var restaurants = await _restRepo.GetRestaurantsAsync();
        var restaurantDto = restaurants.Select(RestaurantDto.FromEntity);
        return restaurantDto!;
    }
}
