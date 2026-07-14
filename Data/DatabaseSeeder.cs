using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Data;

public static class DatabaseSeeder
{
    public static void Seed(InventoryDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Vehicles.Any())
            return;

        var vehicles = new List<Vehicle>
        {
            new()
            {
                Make = "Toyota",
                Model = "Camry",
                Year = 2022,
                DateAddedToInventory = DateTime.UtcNow.AddDays(-120) // Aging stock
            },
            new()
            {
                Make = "Honda",
                Model = "Civic",
                Year = 2024,
                DateAddedToInventory = DateTime.UtcNow.AddDays(-10) // Recent
            },
            new()
            {
                Make = "Ford",
                Model = "F-150",
                Year = 2023,
                DateAddedToInventory = DateTime.UtcNow.AddDays(-95) // Aging stock
            },
            new()
            {
                Make = "BMW",
                Model = "X5",
                Year = 2024,
                DateAddedToInventory = DateTime.UtcNow.AddDays(-5) // Recent
            }
        };

        context.Vehicles.AddRange(vehicles);
        context.SaveChanges();
    }
}
