using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
{
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IRestaurantRepository _restRepo;
    private readonly IMapper _mapper;
    public CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IRestaurantRepository restRepo, IMapper mapper)
    {
        _logger = logger;
        _restRepo = restRepo;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating Restaurant");
        var rest = _mapper.Map<Domain.Entities.Restaurant>(request);
        _logger.LogInformation($"Created Restaurant with name {rest.Name} and id is {rest.Id}");
        return await _restRepo.CreateAsync(rest);
    }
}
