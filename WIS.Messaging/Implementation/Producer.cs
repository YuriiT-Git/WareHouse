using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using DotPulsar.Schemas;
using WIS.Messaging.Abstractions;

namespace WIS.Messaging.Implementation;

public class Producer<T>: PulsarBase, IPulsarProducer<T> where T: class
{
    private IProducer<T> _producer;
    
    public Producer(string serviceUrl, string topicName):base(serviceUrl)
    {
        _producer = Client.NewProducer(JsonSchema.Get<T>())
            .Topic(topicName)
            .Create();
    }

    public async Task ProduceAsync(T item)
    {
        var result = await _producer.Send(item);
        Console.WriteLine($"MesageId: {result}");
    }

    public new async ValueTask DisposeAsync()
    {
        await _producer.DisposeAsync();
        await base.DisposeAsync();
    }
}