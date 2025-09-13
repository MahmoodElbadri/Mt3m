namespace Restaurant.Domain.Repositories;
using Restaurant.Domain.Entities;
public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
}
