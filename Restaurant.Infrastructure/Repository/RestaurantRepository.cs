using Restaurant.Domain.Repositories;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Infrastructure.Repository;

public class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantRepository
{
    public async Task<Domain.Entities.Restaurant?> GetRestaurantByIdAsync(int id)
    {
        var rest = await dbContext.Restaurants.Include(tmp => tmp.Dishes).FirstOrDefaultAsync(tmp => tmp.Id == id);
        return rest;
    }

    public async Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurantsAsync()
    {
        var restaurants = await dbContext.Restaurants.Include(tmp => tmp.Dishes).ToListAsync();
        return restaurants;
    }
}
