using MediatR;

namespace Restaurant.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForResaurantCommand(int restaurantId):IRequest<Unit>
{
    public int RestaurantId => restaurantId;
}
