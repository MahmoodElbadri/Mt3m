using Restaurant.Application.Restaurant.Dtos;

namespace Restaurant.Application.Restaurants;

public interface IRestaurantService
{
    public Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync();
    public Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
    Task<int> CreateAsync(RestaurantCreateDto restDto);
}
