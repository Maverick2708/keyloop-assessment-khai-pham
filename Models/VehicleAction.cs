namespace InventoryDashboard.Api.Models;

public class VehicleAction
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public string ProposedAction { get; set; } = string.Empty;
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public Vehicle Vehicle { get; set; } = null!;
}
