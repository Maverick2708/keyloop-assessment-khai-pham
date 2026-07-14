using Microsoft.EntityFrameworkCore;
using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Services;

public class InventoryService : IInventoryService
{
    private readonly InventoryDbContext _context;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(InventoryDbContext context, ILogger<InventoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Vehicle>> GetInventoryAsync()
    {
        var vehicles = await _context.Vehicles
            .Include(v => v.VehicleActions)
            .ToListAsync();

        _logger.LogInformation("Successfully retrieved {Count} vehicles from inventory.", vehicles.Count);

        return vehicles;
    }

    public async Task<VehicleAction> AddVehicleActionAsync(int vehicleId, string proposedAction)
    {
        var vehicle = await _context.Vehicles.FindAsync(vehicleId);
        if (vehicle is null)
        {
            _logger.LogWarning("Attempted to add action to non-existent vehicle. VehicleId: {VehicleId}", vehicleId);
            throw new KeyNotFoundException($"Vehicle with Id {vehicleId} not found.");
        }

        var action = new VehicleAction
        {
            VehicleId = vehicleId,
            ProposedAction = proposedAction
        };

        _context.VehicleActions.Add(action);
        await _context.SaveChangesAsync();

        return action;
    }
}
