using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurant.Dtos;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Quries.GetRestaurant;

public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, RestaurantDto?>
{
    private readonly ILogger<GetRestaurantQueryHandler> _logger;
    private readonly IRestaurantRepository _restRepo;
    private readonly IMapper _mapper;

    public GetRestaurantQueryHandler(ILogger<GetRestaurantQueryHandler> logger, IRestaurantRepository restRepo, IMapper mapper)
    {
        _mapper = mapper;
        _restRepo = restRepo;
        _logger = logger;

    }
    public async Task<RestaurantDto?> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting Restaurant by {request.Id}");
        var rest = await _restRepo.GetRestaurantByIdAsync(request.Id);
        var restaurantDto = _mapper.Map<RestaurantDto?>(rest);
        return restaurantDto;
    }
}
