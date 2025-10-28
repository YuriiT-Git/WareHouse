namespace MedistR.Abstractions;

public interface IRequestHandler<in TRequest>
{
    public Task Handle(TRequest command, CancellationToken cancellationToken);
}


public interface IRequestHandler<in TRequest, TResult> where TRequest : IRequest<TResult>
{
    public Task<TResult> Handle(TRequest command, CancellationToken cancellationToken);
}