

namespace WIS.Messaging.Abstractions;

public interface IPulsarProducer<T>: IAsyncDisposable where T: class
{
    Task ProduceAsync(T item);
}