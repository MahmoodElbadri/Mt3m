using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.Dtos;
using Restaurant.Application.Dishes.Queries.GetAllDishes;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(ILogger<GetAllDishesCommandHandler> _logger,
    IDishesRepository _dishesRepository,
    IRestaurantRepository _restaurantsRepository,
    IMapper _mapper) : IRequestHandler<GetDishesForRestaurantQuery, DishDto>
{

    async Task<DishDto> IRequestHandler<GetDishesForRestaurantQuery, DishDto>.Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all dishes for restaurant with id: {restaurantId}", request.RestaurantId);
        var restaurant = await _restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant == null)
        {
            _logger.LogWarning("Restaurant with id: {restaurantId} not found", request.RestaurantId);
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());
        }
        var dishes = await _dishesRepository.GetDishesForRestaurantAsync(request.RestaurantId);
        return _mapper.Map<DishDto>(dishes);
    }
}
