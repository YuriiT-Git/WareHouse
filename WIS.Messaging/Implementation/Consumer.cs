using System.Text.Json;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using DotPulsar.Schemas;
using Microsoft.Extensions.Hosting;
using WIS.Messaging.Abstractions;

namespace WIS.Messaging.Implementation;

public class Consumer<T>:PulsarBase, IPulsarConsumer<T>, IHostedService where T: class
{
    private Func<T, Task> _subscriber;
    private readonly IConsumer<T> _consumer;
    private Task<Task> _backgroundTask;


    public Consumer(string serviceUrl, string topicName) : base(serviceUrl)
    {
        var jsonOpts = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        jsonOpts.Converters.Add(new JsonValueConverter());
        
        var schema = JsonSchema.Get<T>(jsonOpts);
        
        _consumer = Client.NewConsumer(schema)
            .Topic(topicName)
            .SubscriptionName("demo-sub")
            .SubscriptionType(SubscriptionType.KeyShared)
            .Create();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _backgroundTask = Task.Factory.StartNew(() => ProcessMessageAsync(cancellationToken), TaskCreationOptions.LongRunning);
        return Task.CompletedTask;
    }

    public void Subscribe(Func<T, Task> subscriber)
    {
        _subscriber = subscriber;
    }

    public new async ValueTask DisposeAsync()
    {
        await _backgroundTask;
        await _consumer.DisposeAsync();
        await base.DisposeAsync();
    }
    
    
    private async Task ProcessMessageAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var message = await _consumer.Receive(cancellationToken);

            try
            {
                await _subscriber?.Invoke(message.Value());
                await _consumer.Acknowledge(message, cancellationToken);
            }
            catch (Exception e)
            {
               // DLQ ?
            }
           
        }
            
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _backgroundTask;
    }
}