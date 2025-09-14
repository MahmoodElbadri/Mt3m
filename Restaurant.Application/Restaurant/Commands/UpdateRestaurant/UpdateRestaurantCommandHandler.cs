using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommand> _logger, IRestaurantRepository _restaurantRepository, IMapper _mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{

    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetRestaurantByIdAsync(request.Id);
        if(restaurant == null)
        {
            _logger.LogError("Restaurant not found");
            return false;
        }
        _mapper.Map(request, restaurant);
        await _restaurantRepository.UpdateAsync(restaurant);
        return true;
    }
}
