
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants;

public class RestaurantService(IRestaurantRepository _restRepo)
{
    public Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurants()
    {
        var restaurants = _restRepo.GetRestaurants();
        return restaurants;
    }
}
