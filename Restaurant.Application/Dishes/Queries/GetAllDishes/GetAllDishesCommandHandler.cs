using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.Dtos;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.GetAllDishes;

public class GetAllDishesCommandHandler(ILogger<GetAllDishesCommandHandler> _logger,
    IDishesRepository _dishesRepository,
    IRestaurantRepository _restaurantsRepository,
    IMapper _mapper) : IRequestHandler<GetAllDishesCommand, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting all dishes for restaurant {request.RestaurantId}");
        var restaurant = await _restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant == null)
        {
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());
        }
        var dishes = restaurant.Dishes;
        var dishesDto = _mapper.Map<IEnumerable<DishDto>>(dishes);
        return dishesDto;
    }
}
