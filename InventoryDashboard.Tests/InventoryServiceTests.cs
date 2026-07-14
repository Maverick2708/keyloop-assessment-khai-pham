using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Models;
using InventoryDashboard.Api.Services;

namespace InventoryDashboard.Tests;

public class InventoryServiceTests : IDisposable
{
    private readonly InventoryDbContext _context;
    private readonly InventoryService _service;

    public InventoryServiceTests()
    {
        var options = new DbContextOptionsBuilder<InventoryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new InventoryDbContext(options);
        var logger = new Mock<ILogger<InventoryService>>();
        _service = new InventoryService(_context, logger.Object);

        SeedTestData();
    }

    private void SeedTestData()
    {
        _context.Vehicles.AddRange(
            new Vehicle
            {
                Id = 1,
                Make = "Toyota",
                Model = "Camry",
                Year = 2022,
                DateAddedToInventory = DateTime.UtcNow.AddDays(-100)
            },
            new Vehicle
            {
                Id = 2,
                Make = "Honda",
                Model = "Civic",
                Year = 2024,
                DateAddedToInventory = DateTime.UtcNow.AddDays(-10)
            }
        );

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetInventoryAsync_ReturnsAllSeededVehicles()
    {
        var result = await _service.GetInventoryAsync();

        var vehicles = result.ToList();
        Assert.Equal(2, vehicles.Count);
    }

    [Fact]
    public async Task AddVehicleActionAsync_CreatesActionSuccessfully()
    {
        var action = await _service.AddVehicleActionAsync(1, "Reduce price by 10%");

        Assert.NotNull(action);
        Assert.Equal(1, action.VehicleId);
        Assert.Equal("Reduce price by 10%", action.ProposedAction);

        var savedAction = await _context.VehicleActions.FirstOrDefaultAsync(a => a.Id == action.Id);
        Assert.NotNull(savedAction);
    }

    [Fact]
    public async Task AddVehicleActionAsync_ThrowsKeyNotFoundException_WhenVehicleDoesNotExist()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.AddVehicleActionAsync(999, "Invalid action"));
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
