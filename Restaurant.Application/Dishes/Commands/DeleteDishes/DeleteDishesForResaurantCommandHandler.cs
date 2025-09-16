using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForResaurantCommandHandler(ILogger<CreateDishCommandHandler> _logger, IRestaurantRepository _restRepo,
    IDishesRepository _dishRepo, IMapper _mapper) : IRequestHandler<DeleteDishesForResaurantCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDishesForResaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Delete Dishes For Resaurant {request.RestaurantId}");
        var restaurant = await _restRepo.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant is null)
        {
            _logger.LogInformation($"Restaurant {request.RestaurantId} Not Found");
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        await _dishRepo.DeleteDishesForResaurantAsync(request.RestaurantId);
        return Unit.Value;
    }
}
