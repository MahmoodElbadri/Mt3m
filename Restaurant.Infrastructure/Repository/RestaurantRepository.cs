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

    public async Task<IEnumerable<Restaurant.Domain.Entities.Restaurant>> GetRestaurantsMatchingAsync(string? searchPhrase, int page, int pageSize)
    {
        IQueryable<Restaurant.Domain.Entities.Restaurant> query = _dbContext.Restaurants.Include(tmp => tmp.Dishes);
        if (!string.IsNullOrWhiteSpace(searchPhrase))
        {
            query = query.Where(tmp => tmp.Name.Contains(searchPhrase, StringComparison.OrdinalIgnoreCase)
            || tmp.Description.Contains(searchPhrase, StringComparison.OrdinalIgnoreCase)
            );
        }
        if (page > 0 && pageSize > 0)
        {
            query = query.Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
        return await query.ToListAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Restaurant restaurant)
    {
        _dbContext.Restaurants.Update(restaurant);
        await _dbContext.SaveChangesAsync();
    }
}
