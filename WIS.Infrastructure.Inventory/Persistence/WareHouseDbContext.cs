using Microsoft.EntityFrameworkCore;
using WIS.Infrastructure.Persistence.Configurations;
using WIS.Infrastructure.Persistence.Repositories;

namespace WIS.Infrastructure.Persistence;

public class WareHouseDbContext(DbContextOptions<WareHouseDbContext> options) : DbContext(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventStorageRepository).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}