using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIS.Application.AuditService;
using WIS.Application.Common.Common.Abstractions;
using WIS.Infrastructure.Audit.Repositories;

namespace WIS.Infrastructure.Audit.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuditDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AuditDatabase")));

      
        services.AddScoped<IInventoryAuditLogRepository, InventoryAuditLogRepository>();
        
        return services;
    }
    
    public static void ApplyAuditMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AuditDbContext>();
            
        if(context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}