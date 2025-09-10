using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restService;

    public RestaurantsController(IRestaurantService restService)
    {
        this._restService = restService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRestaurant()
    {
        var restaurants = await _restService.GetRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestById([FromRoute] int id)
    {
        var rest = await _restService.GetRestaurantById(id);
        if (rest == null)
        {
            return NotFound("Restaurant not found!");
        }
        return Ok(rest);
    }
}