using Restaurant.Application.Dishes.Dtos;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurant.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public bool HasDelivery { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public List<DishDto>? Dishes { get; set; }


    /// <summary>
    /// Converts Domain.Entities.Restaurant to RestaurantDto Deprecated
    /// </summary>
    /// <param name="restaurant"></param>
    /// <returns></returns>
    //public static RestaurantDto? FromEntity(Domain.Entities.Restaurant? restaurant)
    //{
    //    if (restaurant is not null)
    //        return new RestaurantDto
    //        {
    //            Id = restaurant.Id,
    //            Name = restaurant.Name,
    //            Description = restaurant.Description,
    //            Category = restaurant.Category,
    //            HasDelivery = restaurant.HasDelivery,
    //            Street = restaurant.Address?.Street,
    //            City = restaurant.Address?.City,
    //            PostalCode = restaurant.Address?.PostalCode,
    //            Dishes = restaurant.Dishes?.Select(DishDto.FromEntity).ToList()
    //        };
    //    return null;
    //}
}
