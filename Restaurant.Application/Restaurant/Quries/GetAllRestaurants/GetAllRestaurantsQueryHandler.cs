using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurant.Dtos;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Quries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(IMapper _mapper, IRestaurantRepository _restRepo, ILogger<GetAllRestaurantsQueryHandler> _logger) : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
{

    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all Restaurants");
        var restaurants = await _restRepo.GetRestaurantsMatchingAsync(request.searchPhrase);
        
        var restaurantDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantDto!;
    }
}
