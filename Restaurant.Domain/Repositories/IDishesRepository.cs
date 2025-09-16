using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories;

public interface IDishesRepository
{
    Task<int> CreateDishAsync(Dish dish);
}
