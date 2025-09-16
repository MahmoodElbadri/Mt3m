using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Infrastructure.Repository;

public class DishesRepository(RestaurantDbContext _dbContext) : IDishesRepository
{
    public async Task<int> CreateDishAsync(Dish dish)
    {
        await _dbContext.Dishes.AddAsync(dish);
        await _dbContext.SaveChangesAsync();
        return dish.Id;
    }

    public async Task DeleteDishesForResaurantAsync(int restaurantId)
    {
        //delete all dishes for this restaurant
        var dishes = _dbContext.Dishes.Where(tmp => tmp.RestaurantId == restaurantId);
        _dbContext.Dishes.RemoveRange(dishes);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Dish?> GetDishesForRestaurantAsync(int restaurantId)
    {
        return await _dbContext.Dishes.FirstOrDefaultAsync(tmp => tmp.RestaurantId == restaurantId);
    }
}
