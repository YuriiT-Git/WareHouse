using Microsoft.Extensions.DependencyInjection;
using WIS.Application.Common.EventPublisher;

namespace WIS.Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher.EventPublisher>();
        return services;
    }
}