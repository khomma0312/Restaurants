using System.Data;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext: DbContext
{
  internal DbSet<Restaurant> Restaurants { get; set;  }
  internal DbSet<Dish> Dishes { get; set;  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer("Server=localhost,1433;Database=RestaurantsDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;Encrypt=false");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Restaurant>()
    .OwnsOne(r => r.Address);

    modelBuilder.Entity<Restaurant>()
    .HasMany(r => r.Dishes)
    .WithOne()
    .HasForeignKey(d => d.RestaurantId);
  }
}