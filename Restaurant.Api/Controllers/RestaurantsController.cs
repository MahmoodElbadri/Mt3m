using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurant.Commands.CreateRestaurant;
using Restaurant.Application.Restaurant.Commands.DeleteRestaurant;
using Restaurant.Application.Restaurant.Commands.UpdateRestaurant;
using Restaurant.Application.Restaurant.Dtos;
using Restaurant.Application.Restaurant.Quries.GetAllRestaurants;
using Restaurant.Application.Restaurant.Quries.GetRestaurant;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RestaurantsController(IMediator _mediator) : ControllerBase
{


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK),]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurant() //putting ActionResult because wee want to put it in swagger
    {
        var restaurants = await _mediator.Send(new GetAllRestaurantQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> CreateRestaurant([FromBody] CreateRestaurantCommand restaurantCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id = await _mediator.Send(restaurantCommand); //we didn't add the name of the methodod because it is the only method in the command handler
        return CreatedAtAction(nameof(GetRestById), new { id = id }, id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        // Implementation for deleting a restaurant would go here
        await _mediator.Send(new DeleteRestaurantCommand() { Id = id });
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _mediator.Send(new UpdateRestaurantCommand() { Id = id, Description = command.Description, HasDelivery = command.HasDelivery, Name = command.Name });
        return NoContent();
    }
}
