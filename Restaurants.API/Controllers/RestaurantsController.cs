using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantsService restaurantsService): ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
  {
    var restaurants = await restaurantsService.GetAllRestaurants();
    return Ok(restaurants);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<RestaurantDto?>> GetItemById([FromRoute]int id)
  {
    var restaurants = await restaurantsService.GetRestaurantsById(id);
    return Ok(restaurants);
  }

  [HttpPost]
  public async Task<IActionResult> CreateRestaurant([FromBody]CreateRestaurantDto createRestaurantDto)
  {
    int id = await restaurantsService.Create(createRestaurantDto);
    return CreatedAtAction(nameof(GetItemById), new { id }, null);
  }
}