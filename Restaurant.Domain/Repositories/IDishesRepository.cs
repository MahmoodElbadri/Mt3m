using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories;

public interface IDishesRepository
{
    Task<int> CreateDishAsync(Dish dish);
    Task DeleteDishesForResaurantAsync(int restaurantId);
    Task<Dish?> GetDishesForRestaurantAsync(int restaurantId);
}
