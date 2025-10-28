using Microsoft.EntityFrameworkCore;
using WIS.Infrastructure.Configurations;
using WIS.Infrastructure.Entities;

namespace WIS.Infrastructure;

public class WareHouseDbContext(DbContextOptions<WareHouseDbContext> options) : DbContext(options)
{
    public DbSet<InventoryItemModel> InventoryItems { get; set; }
    public DbSet<InventoryStockModel> InventoryStocks { get; set; }
    public DbSet<InventoryAuditLogDataModel> InventoryAuditLog { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InventoryItemModelConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryStockModelConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryAuditLogDataModelConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}