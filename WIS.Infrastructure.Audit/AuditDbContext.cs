using Microsoft.EntityFrameworkCore;
using WIS.Infrastructure.Audit.Repositories;

namespace WIS.Infrastructure.Audit;

public class AuditDbContext(DbContextOptions<AuditDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryAuditLogRepository).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}