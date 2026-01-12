using Restaurants.API.Controllers;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
// Buildが実行された後にはserviceを登録することができないので注意

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

// データベースのシードを実行
await seeder.Seed();

// Configure the HTTP request pipeline.
// ミドルウェアを追加

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
