using Microsoft.Extensions.DependencyInjection;
using Nexus;
using Nexus.Abstractions;
using WIS.Application.Commands;
using WIS.Application.DTO;
using WIS.Application.Handlers;
using WIS.Application.Queries;

namespace WIS.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCommandQueryHandlers(this IServiceCollection services)
    {
        services.AddScoped<INexus, Nexus.Nexus>();
        services.AddTransient<ICommandHandler<CreateInventoryItemCommand, string>, CreateInventoryItemCommandHandler>();
        
        services.AddTransient<ICommandHandler<GetInventoryDetailsQuery, InventoryItemInfoDto>, GetInventoryItemsDetailsHandler>();
        services.AddTransient<ICommandHandler<GetAllInventoryItems, IReadOnlyCollection<InventoryItemInfoDto>>, GetAllInventoryItemsHandler>();
        
        services.AddTransient<ICommandHandler<RegisterIncomingStockCommand, bool>, RegisterIncomingStockCommandHandler>();
        services.AddTransient<ICommandHandler<RegisterOutgoingStockCommand, bool>, RegisterOutgoingStockCommandHandler>();
        
        
        return services;
    }
}