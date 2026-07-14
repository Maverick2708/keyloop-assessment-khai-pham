using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Services;

public interface IInventoryService
{
    Task<IEnumerable<Vehicle>> GetInventoryAsync();
    Task<VehicleAction> AddVehicleActionAsync(int vehicleId, string proposedAction);
}
