using Microsoft.Extensions.DependencyInjection;
using WIS.Application.EventPublisher;

namespace WIS.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher.EventPublisher>();
        return services;
    }
}