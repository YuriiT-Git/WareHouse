using DotPulsar;
using DotPulsar.Abstractions;

namespace WIS.Messaging.Implementation;

public abstract class PulsarBase: IAsyncDisposable
{
    private readonly string _serviceUrl;
   
    protected IPulsarClient Client { get;private set; }

    protected PulsarBase(string serviceUrl)
    {
        _serviceUrl = serviceUrl;
        Client = PulsarClient.Builder()
            .ServiceUrl(new Uri(_serviceUrl))
            .Build();
    }
    
    public async ValueTask DisposeAsync()
    {
        await Client.DisposeAsync();
    }
}