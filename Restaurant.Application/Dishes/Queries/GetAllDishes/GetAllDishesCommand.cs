using MediatR;
using Restaurant.Application.Dishes.Dtos;

namespace Restaurant.Application.Dishes.Queries.GetAllDishes;

public class GetAllDishesCommand:IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; set; }
}
