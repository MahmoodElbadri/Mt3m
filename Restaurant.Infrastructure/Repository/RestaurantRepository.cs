using Restaurant.Domain.Repositories;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Infrastructure.Repository;

public class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurants()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }
}
