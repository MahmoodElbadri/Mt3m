
using MediatR;

namespace Restaurant.Application.Restaurant.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand:IRequest<bool>
{
    public int Id { get; set; }
}
