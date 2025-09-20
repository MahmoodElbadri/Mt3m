namespace Restaurant.Domain.Repositories;
using Restaurant.Domain.Entities;
public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
    Task<int> CreateAsync(Restaurant rest);
    Task<bool> DeleteAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task<IEnumerable<Restaurant>> GetRestaurantsMatchingAsync(string? searchPhrase);
}
