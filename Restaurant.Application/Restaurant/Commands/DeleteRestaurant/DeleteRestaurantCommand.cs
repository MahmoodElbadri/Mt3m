using MediatR;

namespace Restaurant.Application.Restaurant.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand:IRequest<Unit>
{
    public int Id { get; set; }
}
