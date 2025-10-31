using Microsoft.EntityFrameworkCore;
using WIS.Infrastructure.Configurations;

namespace WIS.Infrastructure;

public class WareHouseDbContext(DbContextOptions<WareHouseDbContext> options) : DbContext(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryAuditLogDataModelConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}