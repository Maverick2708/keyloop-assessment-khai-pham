using Microsoft.EntityFrameworkCore;
using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleAction> VehicleActions => Set<VehicleAction>();
}
