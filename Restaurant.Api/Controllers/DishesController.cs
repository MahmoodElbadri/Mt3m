using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.Dtos;

namespace Restaurant.Api.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/restaurants/{restaurantId}/[controller]")]
[ApiController]
public class DishesController (IMediator _mediator): ControllerBase
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
    public async Task<ActionResult<IEnumerable< DishDto>>> GetDishes([FromRoute]int restaurantId)
    {
       var dishes =await _mediator.Send(new GetAllDishesCommand() { RestaurantId = restaurantId });
        return Ok(dishes);
    }
}
