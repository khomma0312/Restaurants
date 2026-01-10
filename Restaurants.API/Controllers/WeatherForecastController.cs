using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherForecastService weatherForecastService, ILogger<WeatherForecastController> logger)
    {
        _weatherForecastService = weatherForecastService;
        _logger = logger;
    }

    [HttpGet]
    [Route("{take}/example")]
    public IEnumerable<WeatherForecast> Get([FromQuery]int max, [FromRoute]int take)
    {
        _logger.LogInformation($"Get called with max={max}, take={take}");
        var result = _weatherForecastService.Get();
        _logger.LogInformation($"Returning {result.Count()} items");
        return result;
    }

    [HttpGet("currentDay")]
    public IActionResult GetCurrentDayForecast()
    {
        var result = _weatherForecastService.Get().First();

        return Ok(result);
    }

    [HttpPost()]
    public string Hello([FromBody] string name)
    {
        return $"Hello {name}!";
    }

    [HttpPost("generate")]
    public IActionResult Generate([FromBody]GenerateRequest request, [FromQuery]int count)
{
    if (count <= 0 || request.MinDegree >= request.MaxDegree)
    {
        return BadRequest("Invalid parameters. Check that count is positive and MinDegree is less than MaxDegree.");
    }

    var forecasts = _weatherForecastService.Get(request.MinDegree, request.MaxDegree, count);
    return Ok(forecasts);
}
}

// DTOクラスを作成
public class GenerateRequest
{
    public int MinDegree { get; set; }
    public int MaxDegree { get; set; }
}