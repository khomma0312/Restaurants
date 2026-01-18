using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger, IMapper mapper): IRestaurantsService
{
  public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
  {
    logger.LogInformation("Getting all restaurants");
    var restaurants = await restaurantsRepository.GetAllAsync();

    var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
    return restaurantDtos!;
  }

  public async Task<RestaurantDto> GetRestaurantsById(int id)
  {
    logger.LogInformation("Getting restaurant by id: {Id}", id);
    var restaurant = await restaurantsRepository.GetById(id);

    var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
    if (restaurantDto == null)
        throw new NotFoundException(nameof(Restaurant), id.ToString());

    return restaurantDto;
  }

  public async Task<int> Create(CreateRestaurantDto dto)
  {
    logger.LogInformation("Creating a new restaurant");
    var restaurant = mapper.Map<Restaurant>(dto);
    int id = await restaurantsRepository.Create(restaurant);
    return id;
  }
}