namespace Restaurant.Domain.Repositories;
using Restaurant.Domain.Entities;
public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurants();
    Task<Restaurant?> GetRestaurantById(int id);
}
