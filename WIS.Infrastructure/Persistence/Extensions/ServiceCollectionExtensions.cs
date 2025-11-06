using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIS.Application.Common.Common.Abstractions;
using WIS.Infrastructure.Persistence.Repositories;

namespace WIS.Infrastructure.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WareHouseDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("WareHouseDatabase")));

        services.AddScoped<IEventStorageRepository, EventStorageRepository>();
        services.AddScoped<IInventoryAuditLogRepository, InventoryAuditLogRepository>();
        
        return services;
    }
    
    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WareHouseDbContext>();
            
        if(context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}