using Restaurant.Domain.Repositories;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Infrastructure.Repository;

public class RestaurantRepository(RestaurantDbContext _dbContext) : IRestaurantRepository
{
    public async Task<int> CreateAsync(Domain.Entities.Restaurant rest)
    {
        await _dbContext.Restaurants.AddAsync(rest);
        await _dbContext.SaveChangesAsync();
        return rest.Id;
    }

    public async Task<bool> DeleteAsync(Domain.Entities.Restaurant restaurant)
    {

        _dbContext.Restaurants.Remove(restaurant);
        await _dbContext.SaveChangesAsync();
        return true;

    }

    public async Task<Domain.Entities.Restaurant?> GetRestaurantByIdAsync(int id)
    {
        var rest = await _dbContext.Restaurants.Include(tmp => tmp.Dishes).FirstOrDefaultAsync(tmp => tmp.Id == id);
        return rest;
    }

    public async Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurantsAsync()
    {
        var restaurants = await _dbContext.Restaurants.Include(tmp => tmp.Dishes).ToListAsync();
        return restaurants;
    }

    public async Task UpdateAsync(Domain.Entities.Restaurant restaurant)
    {
        _dbContext.Restaurants.Update(restaurant);
        await _dbContext.SaveChangesAsync();
    }
}
