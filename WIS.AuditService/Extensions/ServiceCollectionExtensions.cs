using WIS.Domain.Events;
using WIS.Messaging.Abstractions;
using WIS.Messaging.Configuration;
using WIS.Messaging.Implementation;

namespace WIS.AuditService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var messagingSection = configuration.GetSection(nameof(MessagingConfig)).Get<MessagingConfig>();

        var serviceUrl = messagingSection.ServiceUrl;
      
        services.AddSingleton(new Consumer<StockUpdatedEvent>(serviceUrl, messagingSection.Consumer.Topic));
        services.AddSingleton<IPulsarConsumer<StockUpdatedEvent>>(x => x.GetRequiredService<Consumer<StockUpdatedEvent>>());
        services.AddHostedService(x => x.GetRequiredService<Consumer<StockUpdatedEvent>>());
        return services;
    }
}