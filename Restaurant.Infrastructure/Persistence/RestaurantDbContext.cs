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
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    optionsBuilder.UseSqlServer("");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Domain.Entities.Restaurant>(tmp =>
        {
            tmp.OwnsOne(tmp => tmp.Address)
            .Property(tmp => tmp.Street)
            .IsRequired()
            .HasMaxLength(100);

            tmp.OwnsOne(tmp => tmp.Address)
            .Property(tmp => tmp.City)
            .IsRequired()
            .HasMaxLength(100);

            tmp.OwnsOne(tmp => tmp.Address)
            .Property(tmp => tmp.PostalCode)
            .HasMaxLength(10);
        });



        modelBuilder.Entity<Restaurant.Domain.Entities.Restaurant>()
            .HasMany(tmp => tmp.Dishes)
            .WithOne()
            .HasForeignKey(tmp => tmp.RestaurantId);

        modelBuilder.Entity<Restaurant.Domain.Entities.Dish>()
            .Property(tmp => tmp.Price)
            .HasPrecision(18, 2);



    }
}
