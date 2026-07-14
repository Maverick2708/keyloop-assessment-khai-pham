using Xunit;
using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Tests;

public class VehicleTests
{
    [Fact]
    public void IsAgingStock_ReturnsTrue_WhenVehicleInInventoryMoreThan90Days()
    {
        var vehicle = new Vehicle
        {
            Id = 1,
            Make = "Toyota",
            Model = "Camry",
            Year = 2023,
            DateAddedToInventory = DateTime.UtcNow.AddDays(-91)
        };

        Assert.True(vehicle.IsAgingStock);
    }

    [Fact]
    public void IsAgingStock_ReturnsFalse_WhenVehicleInInventoryLessThan90Days()
    {
        var vehicle = new Vehicle
        {
            Id = 2,
            Make = "Honda",
            Model = "Civic",
            Year = 2024,
            DateAddedToInventory = DateTime.UtcNow.AddDays(-89)
        };

        Assert.False(vehicle.IsAgingStock);
    }
}
