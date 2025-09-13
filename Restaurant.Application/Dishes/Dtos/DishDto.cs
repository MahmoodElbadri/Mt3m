using Restaurant.Domain.Entities;

namespace Restaurant.Application.Dishes.Dtos;

public class DishDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? KillCalories { get; set; }

    /// <summary>
    /// Convert Dish to DishDtonverts Domain.Entities.Dish to DishDto Deprecated - Use AutoMapper instead
    /// </summary>
    /// <param name="dish"></param>
    /// <returns></returns>

    //public static DishDto FromEntity(Dish dish) => new()
    //{
    //    Id = dish.Id,
    //    Name = dish.Name,
    //    Description = dish.Description,
    //    Price = dish.Price,
    //    KillCalories = dish.KillCalories
    //};
}
