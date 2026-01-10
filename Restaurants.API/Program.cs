using Restaurants.API.Controllers;
using Restaurants.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
// Buildが実行された後にはserviceを登録することができないので注意

// Configure the HTTP request pipeline.
// ミドルウェアを追加

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
