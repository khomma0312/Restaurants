namespace Restaurants.API.Controllers;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get(int minDegree = -20, int maxDegree = 55, int count = 5);
}

public class WeatherForecastService: IWeatherForecastService
{
  private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
  public IEnumerable<WeatherForecast> Get(int minDegree, int maxDegree, int count)
    {
        return Enumerable.Range(1, count)
            .Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(minDegree, maxDegree + 1),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .Where(w => w.TemperatureC >= minDegree && w.TemperatureC <= maxDegree)
            .ToArray();
    }
}