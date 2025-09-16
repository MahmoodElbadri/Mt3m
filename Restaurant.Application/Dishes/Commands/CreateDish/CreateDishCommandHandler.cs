using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> _logger, IRestaurantRepository _restRepo, IDishesRepository _dishRepo,
    IMapper _mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new dish {@dishRequest}", request);
        int restaurantId = request.RestaurantId;
        var restaurant = await _restRepo.GetRestaurantByIdAsync(restaurantId);
        if(restaurant == null)
        {
            throw new NotFoundException(nameof(restaurant), restaurantId.ToString());
        }

        var dish = _mapper.Map<Dish>(request);

       return  await _dishRepo.CreateDishAsync(dish);
    }
}
