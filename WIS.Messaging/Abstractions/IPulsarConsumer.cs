namespace WIS.Messaging.Abstractions;

public interface IPulsarConsumer<T> : IAsyncDisposable where T: class
{
    void Subscribe(Func<T, Task> subscriber);
}