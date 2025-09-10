namespace Restaurant.Application.Restaurants;

public interface IRestaurantService
{
    public Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurants();
    public Task<Restaurant.Domain.Entities.Restaurant?> GetRestaurantById(int id);
}
