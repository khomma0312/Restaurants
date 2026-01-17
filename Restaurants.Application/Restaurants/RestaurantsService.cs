using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger): IRestaurantsService
{
  public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
  {
    logger.LogInformation("Getting all restaurants");
    var restaurants = await restaurantsRepository.GetAllAsync();
    return restaurants;
  }

  public async Task<Restaurant> GetRestaurantsById(int id)
  {
    logger.LogInformation("Getting restaurant by id: {Id}", id);
    var restaurant = await restaurantsRepository.GetById(id);

    if (restaurant is null)
        throw new NotFoundException(nameof(Restaurant), id.ToString());
    
    return restaurant;
  }
}