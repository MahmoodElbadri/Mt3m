using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, Unit>
{
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IRestaurantRepository _restaurantRepository;

    public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository)
    {
        this._logger = logger;
        this._restaurantRepository = restaurantRepository;
    }
    public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Delete Restaurant {request.Id}");

        var restaurant = await _restaurantRepository.GetRestaurantByIdAsync(request.Id);

        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant {request.Id} not found");
        }

        await _restaurantRepository.DeleteAsync(restaurant);
        return Unit.Value;
    }
}
