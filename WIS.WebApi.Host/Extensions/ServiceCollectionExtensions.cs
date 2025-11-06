using WIS.Domain.Events;
using WIS.Messaging.Abstractions;
using WIS.Messaging.Configuration;
using WIS.Messaging.Implementation;

namespace WarehouseInventorySystem.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var messagingSection = configuration.GetSection(nameof(MessagingConfig)).Get<MessagingConfig>();

        var serviceUrl = messagingSection.ServiceUrl;
        services.AddSingleton<IPulsarProducer<StockUpdatedEvent>>(x => new Producer<StockUpdatedEvent>(serviceUrl, messagingSection.Producer.Topic));
        return services;
    }
}