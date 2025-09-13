using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
{
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IRestaurantRepository _restaurantRepository;

    public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository)
    {
        this._logger = logger;
        this._restaurantRepository = restaurantRepository;
    }
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Delete Restaurant {request.Id}");

        var restaurant = await _restaurantRepository.GetRestaurantByIdAsync(request.Id);

        if (restaurant == null)
        {
            return false;
        }

        bool isDeleted = await _restaurantRepository.DeleteAsync(restaurant);
        return isDeleted;
    }
}
