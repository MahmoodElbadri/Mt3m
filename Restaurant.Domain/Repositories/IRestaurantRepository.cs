namespace Restaurant.Domain.Repositories;
using Restaurant.Domain.Entities;
public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurants();
}
