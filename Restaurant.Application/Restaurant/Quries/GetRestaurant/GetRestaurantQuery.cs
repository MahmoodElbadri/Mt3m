using MediatR;
using Restaurant.Application.Restaurant.Dtos;

namespace Restaurant.Application.Restaurant.Quries.GetRestaurant;

public class GetRestaurantQuery:IRequest<RestaurantDto?>
{

    public GetRestaurantQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
    //there is no need for a constructor no need for constructor because we can use object initializer
}
