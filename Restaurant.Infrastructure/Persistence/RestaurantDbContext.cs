using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Persistence;

public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : DbContext(options)
{
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Restaurant.Domain.Entities.Restaurant> Restaurants { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Domain.Entities.Restaurant>()
            .OwnsOne(tmp => tmp.Address);

        modelBuilder.Entity<Restaurant.Domain.Entities.Restaurant>()
            .HasMany(tmp => tmp.Dishes)
            .WithOne()
            .HasForeignKey(tmp => tmp.RestaurantId);
    }
}
