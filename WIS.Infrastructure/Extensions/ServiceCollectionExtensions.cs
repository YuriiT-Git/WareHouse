using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIS.Application.Common.Abstractions;
using WIS.Infrastructure.Repositories;

namespace WIS.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WareHouseDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("WareHouseDatabase")));

        services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        return services;
    }
    
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WareHouseDbContext>();
            
        if(context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}