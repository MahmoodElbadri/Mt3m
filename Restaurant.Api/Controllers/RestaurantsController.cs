using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurant.Commands.CreateRestaurant;
using Restaurant.Application.Restaurant.Commands.DeleteRestaurant;
using Restaurant.Application.Restaurant.Commands.UpdateRestaurant;
using Restaurant.Application.Restaurant.Dtos;
using Restaurant.Application.Restaurant.Quries.GetAllRestaurants;
using Restaurant.Application.Restaurant.Quries.GetRestaurant;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IMediator _mediator) : ControllerBase
{


    [HttpGet]
    public async Task<IActionResult> GetRestaurant()
    {
        var restaurants = await _mediator.Send(new GetAllRestaurantQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestById([FromRoute] int id)
    {
        var rest = await _mediator.Send(new GetRestaurantQuery(id));
        if (rest == null)
        {
            return NotFound("Restaurant not found!");
        }
        return Ok(rest);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand restaurantCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id = await _mediator.Send(restaurantCommand); //we didn't add the name of the methodod because it is the only method in the command handler
        return CreatedAtAction(nameof(GetRestById), new { id = id }, id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        // Implementation for deleting a restaurant would go here
        var isDeleted = await _mediator.Send(new DeleteRestaurantCommand() { Id = id });
        return (isDeleted) ? NoContent() : NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var isUpdated = await _mediator.Send(new UpdateRestaurantCommand() { Id = id , Description = command.Description , HasDelivery = command.HasDelivery , Name = command.Name });
        return (isUpdated) ? NoContent() : NotFound();
    }
}
