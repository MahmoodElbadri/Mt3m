using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.Dtos;
using Restaurant.Application.Dishes.Queries.GetAllDishes;
using Restaurant.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurant.Application.Restaurant.Quries.GetAllRestaurants;

namespace Restaurant.Api.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/restaurants/{restaurantId}/[controller]")]
[ApiController]
public class DishesController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var id = await _mediator.Send(command);
        //returning the id as json object

        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetDishes([FromRoute] int restaurantId)
    {
        var dishes = await _mediator.Send(new GetAllDishesCommand(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetDishesForARestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var dishes = await _mediator.Send(new GetDishesForRestaurantQuery(restaurantId, dishId));
        return Ok(dishes);
    }
}
