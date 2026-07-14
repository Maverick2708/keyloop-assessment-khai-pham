using Microsoft.AspNetCore.Mvc;
using InventoryDashboard.Api.Models;
using InventoryDashboard.Api.Services;

namespace InventoryDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
    {
        _inventoryService = inventoryService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetInventory()
    {
        var vehicles = await _inventoryService.GetInventoryAsync();
        _logger.LogInformation("Inventory endpoint returned {Count} vehicles.", vehicles.Count());
        return Ok(vehicles);
    }

    [HttpPost("{vehicleId}/actions")]
    public async Task<ActionResult<VehicleAction>> AddVehicleAction(int vehicleId, [FromBody] AddActionRequest request)
    {
        try
        {
            var action = await _inventoryService.AddVehicleActionAsync(vehicleId, request.ProposedAction);
            return CreatedAtAction(nameof(GetInventory), new { id = action.Id }, action);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Action creation failed — vehicle not found. VehicleId: {VehicleId}", vehicleId);
            return NotFound(new { message = ex.Message });
        }
    }
}

public class AddActionRequest
{
    public string ProposedAction { get; set; } = string.Empty;
}
