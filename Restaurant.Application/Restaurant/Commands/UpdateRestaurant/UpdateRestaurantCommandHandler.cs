using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommand> _logger, IRestaurantRepository _restaurantRepository, IMapper _mapper) : IRequestHandler<UpdateRestaurantCommand, Unit>
{

    public async Task<Unit> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)        
    {
        var restaurant = await _restaurantRepository.GetRestaurantByIdAsync(request.Id);
        if (restaurant == null)
        {
            _logger.LogError("Restaurant not found");
            throw new NotFoundException(nameof(restaurant), request.Id.ToString());
        }
        _mapper.Map(request, restaurant);
        await _restaurantRepository.UpdateAsync(restaurant);
        return Unit.Value;
    }
}
