using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Infrastructure.Seeders;

public class RestaurantSeeder : IRestaurantSeeder
{
    private readonly RestaurantDbContext _dbContext;
    private readonly ILogger<RestaurantSeeder>? _logger;

    public RestaurantSeeder(RestaurantDbContext dbContext, ILogger<RestaurantSeeder>? logger = null)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task SeedData()
    {
        try
        {
            Console.WriteLine("=== DETAILED SEEDING DEBUG ===");

            // Step 1: Test database connection
            Console.WriteLine("Step 1: Testing database connection...");
            var canConnect = await _dbContext.Database.CanConnectAsync();
            Console.WriteLine($"✅ Can connect to database: {canConnect}");

            if (!canConnect)
            {
                Console.WriteLine("❌ FATAL: Cannot connect to database!");
                return;
            }

            // Step 2: Ensure database is created
            Console.WriteLine("Step 2: Ensuring database exists...");
            var dbCreated = await _dbContext.Database.EnsureCreatedAsync();
            Console.WriteLine($"✅ Database created/exists: {dbCreated}");

            // Step 3: Check current restaurant count
            Console.WriteLine("Step 3: Checking existing data...");
            var currentCount = await _dbContext.Restaurants.CountAsync();
            Console.WriteLine($"✅ Current restaurants in database: {currentCount}");

            // Step 4: Check dishes count too
            var dishCount = await _dbContext.Dishes.CountAsync();
            Console.WriteLine($"✅ Current dishes in database: {dishCount}");

            if (currentCount > 0)
            {
                Console.WriteLine("⚠️  Restaurants already exist. Do you want to seed anyway? (Skipping for now)");

                // Let's see what's actually in there
                var existingRestaurants = await _dbContext.Restaurants
                    .Select(r => new { r.Id, r.Name })
                    .ToListAsync();

                Console.WriteLine("📋 Existing restaurants:");
                foreach (var restaurant in existingRestaurants)
                {
                    Console.WriteLine($"   - ID: {restaurant.Id}, Name: {restaurant.Name}");
                }
                return;
            }

            // Step 5: Generate test data
            Console.WriteLine("Step 4: Generating restaurant data...");
            var restaurants = GetRestaurants();
            Console.WriteLine($"✅ Generated {restaurants.Count} restaurants");

            // Step 6: Add to context (but don't save yet)
            Console.WriteLine("Step 5: Adding restaurants to DbContext...");
            await _dbContext.Restaurants.AddRangeAsync(restaurants);
            Console.WriteLine("✅ Restaurants added to context");

            // Step 7: Check what's in the context before saving
            var entriesBeforeSave = _dbContext.ChangeTracker.Entries().Count();
            Console.WriteLine($"✅ Entities being tracked before save: {entriesBeforeSave}");

            // Step 8: Save changes
            Console.WriteLine("Step 6: Saving to database...");
            var savedCount = await _dbContext.SaveChangesAsync();
            Console.WriteLine($"🎉 SAVED {savedCount} entities to database!");

            // Step 9: Verify the save worked
            Console.WriteLine("Step 7: Verifying data was saved...");
            var finalCount = await _dbContext.Restaurants.CountAsync();
            var finalDishCount = await _dbContext.Dishes.CountAsync();
            Console.WriteLine($"✅ Final restaurant count: {finalCount}");
            Console.WriteLine($"✅ Final dish count: {finalDishCount}");

            // Step 10: Show sample data
            if (finalCount > 0)
            {
                var sampleRestaurant = await _dbContext.Restaurants
                    .Include(r => r.Dishes)
                    .FirstAsync();

                Console.WriteLine($"📋 Sample restaurant: {sampleRestaurant.Name}");
                Console.WriteLine($"   - Category: {sampleRestaurant.Category}");
                Console.WriteLine($"   - Dishes: {sampleRestaurant.Dishes?.Count ?? 0}");
                if (sampleRestaurant.Address != null)
                {
                    Console.WriteLine($"   - Address: {sampleRestaurant.Address.City}, {sampleRestaurant.Address.Street}");
                }
            }

            Console.WriteLine("🎉🎉🎉 SEEDING COMPLETED SUCCESSFULLY! 🎉🎉🎉");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 SEEDING FAILED!");
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");

            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }

            throw;
        }
    }

    private List<Domain.Entities.Restaurant> GetRestaurants()
    {
        var restaurants = new List<Domain.Entities.Restaurant>()
        {
            // Restaurant 1: KFC
            new()
            {
                // Don't set Id - let EF generate it
                Name = "KFC",
                Description = "KFC is a fast food restaurant that specializes in fried chicken.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contact@kfc.com",
                ContactNumber = "0123456789",
                Address = new Address()
                {
                    City = "Cairo",
                    Street = "Nasr City",
                    PostalCode = "12345"
                },
                Dishes = new List<Dish>()
                {
                    new()
                    {
                        Name = "Chicken Bucket",
                        Description = "A bucket of 9 pieces of crispy fried chicken.",
                        Price = 15.50M
                    },
                    new()
                    {
                        Name = "Zinger Burger",
                        Description = "Spicy chicken fillet burger with lettuce and mayo.",
                        Price = 5.75M
                    }
                }
            },
            // Restaurant 2: Pizza Hut
            new()
            {
                Name = "Pizza Hut",
                Description = "Your favorite place for delicious pizzas, pasta, and wings.",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "orders@pizzahut.eg",
                ContactNumber = "19000",
                Address = new Address()
                {
                    City = "Giza",
                    Street = "Haram Street",
                    PostalCode = "54321"
                },
                Dishes = new List<Dish>()
                {
                    new()
                    {
                        Name = "Margherita Pizza",
                        Description = "Classic pizza with mozzarella cheese and fresh tomato sauce.",
                        Price = 12.00M
                    },
                    new()
                    {
                        Name = "Pepperoni Pizza",
                        Description = "A crowd-pleaser with generous toppings of pepperoni and cheese.",
                        Price = 14.50M
                    }
                }
            },
            // Restaurant 3: Abou Tarek
            new()
            {
                Name = "Abou Tarek",
                Description = "The most famous Koshary restaurant in downtown Cairo, serving authentic Egyptian street food.",
                Category = "Egyptian",
                HasDelivery = false,
                ContactEmail = "info@aboutarek.com",
                ContactNumber = "0111222333",
                Address = new Address()
                {
                    City = "Cairo",
                    Street = "Downtown",
                    PostalCode = "11511"
                },
                Dishes = new List<Dish>()
                {
                    new()
                    {
                        Name = "Koshary",
                        Description = "A traditional Egyptian dish of rice, macaroni, and lentils topped with a spicy tomato sauce and fried onions.",
                        Price = 3.50M
                    }
                }
            }
        };
        return restaurants;
    }
}