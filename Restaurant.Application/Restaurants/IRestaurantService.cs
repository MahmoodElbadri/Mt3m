namespace Restaurant.Application.Restaurants;

public interface IRestaurantService
{
    public Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurants();
}
