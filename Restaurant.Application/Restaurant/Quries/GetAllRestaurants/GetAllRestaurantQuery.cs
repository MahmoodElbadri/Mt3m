using MediatR;
using Restaurant.Application.Restaurant.Dtos;

namespace Restaurant.Application.Restaurant.Quries.GetAllRestaurants;

public class GetAllRestaurantQuery:IRequest<IEnumerable<RestaurantDto>>
{
    //u don't need to add any parameters for this query as it has no parameters
    public string? searchPhrase { get; set; }
}
